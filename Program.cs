
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Load configuration from environment variables for security
        string speechKey = Environment.GetEnvironmentVariable("AZURE_SPEECH_KEY") ?? "<YOUR_SPEECH_KEY>";
        string speechRegion = Environment.GetEnvironmentVariable("AZURE_SPEECH_REGION") ?? "westus2";
        string translatorKey = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_KEY") ?? "<YOUR_TRANSLATOR_KEY>";
        string translatorRegion = Environment.GetEnvironmentVariable("AZURE_TRANSLATOR_REGION") ?? "westus2";
        string translatorEndpoint = $"https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=hi&to=en";

        // Validate that required keys are provided
        if (speechKey == "<YOUR_SPEECH_KEY>" || translatorKey == "<YOUR_TRANSLATOR_KEY>")
        {
            Console.WriteLine("❌ Error: Azure service keys not configured!");
            Console.WriteLine("Please set the following environment variables:");
            Console.WriteLine("  - AZURE_SPEECH_KEY");
            Console.WriteLine("  - AZURE_SPEECH_REGION");
            Console.WriteLine("  - AZURE_TRANSLATOR_KEY");
            Console.WriteLine("  - AZURE_TRANSLATOR_REGION");
            Console.WriteLine("\nFor testing, you can set them temporarily:");
            Console.WriteLine("$env:AZURE_SPEECH_KEY = \"your-key-here\"");
            return;
        }

        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        
        // Create a Hindi text sample for testing (not copyrighted content)
        string hindiText = "नमस्ते दोस्तों, आज का दिन बहुत सुंदर है। मैं हिन्दी में बात कर रहा हूं। यह एक परीक्षण है।";
        // Translation: "Hello friends, today is a very beautiful day. I am speaking in Hindi. This is a test."
        
        Console.WriteLine("🎵 Creating Hindi speech from text...");
        Console.WriteLine($"📝 Hindi text: {hindiText}");
        
        // Step 1: Create audio from Hindi text using text-to-speech
        speechConfig.SpeechSynthesisLanguage = "hi-IN";
        speechConfig.SpeechSynthesisVoiceName = "hi-IN-SwaraNeural";
        
        string tempAudioFile = "temp_hindi_speech.wav";
        var audioOutputConfig = AudioConfig.FromWavFileOutput(tempAudioFile);
        
        SpeechRecognitionResult result;
        
        try
        {
            // Step 1: Generate Hindi speech from text
            Console.WriteLine("🎤 Generating Hindi speech...");
            using (var synthesizer = new SpeechSynthesizer(speechConfig, audioOutputConfig))
            {
                var synthResult = await synthesizer.SpeakTextAsync(hindiText);
                
                if (synthResult.Reason == ResultReason.SynthesizingAudioCompleted)
                {
                    Console.WriteLine("✅ Hindi speech generated successfully!");
                }
                else
                {
                    Console.WriteLine($"❌ Speech synthesis failed: {synthResult.Reason}");
                    return;
                }
            }
            
            // Step 2: Recognize the generated Hindi speech
            Console.WriteLine("\n🎯 Recognizing the generated Hindi speech...");
            speechConfig.SpeechRecognitionLanguage = "hi-IN";
            
            var audioInputConfig = AudioConfig.FromWavFileInput(tempAudioFile);
            using var recognizer = new SpeechRecognizer(speechConfig, audioInputConfig);
            
            result = await recognizer.RecognizeOnceAsync();
            
            Console.WriteLine($"🎯 Recognition result: {result.Reason}");
            
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"✅ Recognized Hindi text: {result.Text}");
            }
            else
            {
                Console.WriteLine($"❌ Recognition failed: {result.Reason}");
                Console.WriteLine($"💡 Using original text for translation demo: {hindiText}");
                // We'll manually set the text for translation
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Error in speech processing: {ex.Message}");
            Console.WriteLine($"💡 Using fallback text for translation demo: {hindiText}");
            result = null!; // We'll handle this case below
        }
        // Clean up temporary file
        try
        {
            if (System.IO.File.Exists(tempAudioFile))
            {
                System.IO.File.Delete(tempAudioFile);
                Console.WriteLine("🧹 Cleaned up temporary audio file");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Could not delete temporary file: {ex.Message}");
        }

        // Process the recognition result or use fallback text
        string textToTranslate = "";
        
        if (result?.Reason == ResultReason.RecognizedSpeech)
        {
            textToTranslate = result.Text;
            Console.WriteLine($"\n📝 Hindi text from speech recognition: {textToTranslate}");
        }
        else
        {
            textToTranslate = hindiText;
            Console.WriteLine($"\n📝 Using fallback Hindi text: {textToTranslate}");
        }
        
        // Translate the Hindi text to English
        Console.WriteLine("\n🔄 Translating to English...");
        var englishText = await TranslateTextAsync(translatorKey, translatorRegion, translatorEndpoint, textToTranslate);
        Console.WriteLine($"🌍 English translation: {englishText}");

        // Speak the English translation
        Console.WriteLine("\n🔊 Speaking English translation...");
        speechConfig.SpeechSynthesisLanguage = "en-US";
        speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";
        using var englishSynthesizer = new SpeechSynthesizer(speechConfig);
        await englishSynthesizer.SpeakTextAsync(englishText);
        
        Console.WriteLine("✅ Translation complete!");

    }

    static async Task<string> TranslateTextAsync(string key, string region, string endpoint, string text)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", region);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var body = new[] { new { Text = text } };
        var response = await client.PostAsJsonAsync(endpoint, body);
        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement[0].GetProperty("translations")[0].GetProperty("text").GetString() ?? "";
    }
}
