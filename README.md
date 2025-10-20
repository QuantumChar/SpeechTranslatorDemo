# 🗣️ Speech Translator Demo

> **An intelligent multilingual speech processing application that bridges language barriers through AI-powered translation**

This .NET 8.0 console application showcases the seamless integration of Azure Cognitive Services to create a complete speech-to-speech translation pipeline. The demo transforms Hindi speech into English through a sophisticated four-stage process: speech synthesis, recognition, translation, and audio output.

## 📖 Description

The Speech Translator Demo represents a practical implementation of modern AI language processing technologies. Built with C# and powered by Microsoft Azure's robust cognitive services, this application demonstrates how cloud-based AI can be leveraged to break down language barriers in real-time communication scenarios.

**Key Capabilities:**
- 🎙️ **Intelligent Speech Processing**: Utilizes Azure's advanced neural networks for high-accuracy speech recognition
- 🌐 **Real-time Translation**: Employs Azure Translator's machine learning models for contextual Hindi-to-English translation  
- 🔊 **Natural Voice Synthesis**: Generates human-like speech output using Azure's neural text-to-speech technology
- 🔄 **End-to-End Pipeline**: Seamlessly chains multiple AI services for a complete translation experience

**Technical Innovation:**
This project demonstrates enterprise-grade integration patterns for cognitive services, proper error handling, and clean architecture principles. It serves as both a functional tool and a learning resource for developers exploring AI-powered language processing solutions.

## � Why This Project Matters

In our increasingly connected world, language barriers remain a significant challenge for global communication. This project addresses this challenge by demonstrating how modern AI technologies can be combined to create practical, real-world solutions for multilingual communication.

**Impact Areas:**
- 🎓 **Education**: Facilitates language learning and cross-cultural education
- 🏢 **Business**: Enables international collaboration and customer service
- ♿ **Accessibility**: Supports individuals with different language capabilities
- 🔬 **Research**: Provides a foundation for advanced speech processing applications

## �🎯 Features

- **🎤 Text-to-Speech Synthesis**: Generates natural-sounding Hindi speech from text using Azure's neural voices
- **🧠 Speech Recognition**: Converts Hindi speech back to text with high accuracy using advanced ML models  
- **🌐 Intelligent Translation**: Translates Hindi text to English with contextual understanding via Azure Translator
- **🔊 Audio Output**: Speaks the English translation aloud using natural-sounding voices
- **🛡️ Error Handling**: Robust exception management and fallback mechanisms
- **🧹 Resource Management**: Automatic cleanup of temporary audio files

## 🛠️ Prerequisites

- .NET 8.0 SDK
- Azure Speech Service subscription
- Azure Translator Service subscription

## 🚀 Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/SpeechTranslatorDemo.git
   cd SpeechTranslatorDemo
   ```

2. **Install dependencies**:
   ```bash
   dotnet restore
   ```

3. **Configure Azure Services**:
   
   **🔐 Secure Method (Recommended)**:
   ```powershell
   # Set environment variables (PowerShell)
   $env:AZURE_SPEECH_KEY = "your-speech-service-key"
   $env:AZURE_SPEECH_REGION = "westus2"
   $env:AZURE_TRANSLATOR_KEY = "your-translator-service-key"  
   $env:AZURE_TRANSLATOR_REGION = "westus2"
   ```
   
   📖 **See [CONFIGURATION.md](CONFIGURATION.md) for detailed setup instructions**

## 🎮 Usage

Run the application:
```bash
dotnet run
```

The application will:
1. Generate Hindi speech from predefined text
2. Recognize the Hindi speech
3. Translate it to English
4. Speak the English translation

## 📝 Sample Output

```
🎵 Creating Hindi speech from text...
📝 Hindi text: नमस्ते दोस्तों, आज का दिन बहुत सुंदर है।
🎤 Generating Hindi speech...
✅ Hindi speech generated successfully!
🎯 Recognizing the generated Hindi speech...
✅ Recognized Hindi text: नमस्ते दोस्तों, आज का दिन बहुत सुंदर है।
🌍 English translation: Hello friends, today is a beautiful day.
🔊 Speaking English translation...
```

## 🔧 Configuration

### Supported Languages
- **Source**: Hindi (hi-IN)
- **Target**: English (en-US)

### Azure Services Used
- **Azure Speech Service**: For text-to-speech and speech recognition
- **Azure Translator**: For Hindi to English translation

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## ⚠️ Security Note

Never commit your Azure service keys to the repository. Use environment variables or Azure Key Vault for production deployments.

## 👨‍💻 Author

**QuantumChar** - [GitHub Profile](https://github.com/QuantumChar)

This project demonstrates Azure Cognitive Services integration for multilingual speech processing.
