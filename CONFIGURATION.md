# 🔐 Secure Configuration Guide

## Setting Up Environment Variables

### For Development (PowerShell):
```powershell
# Set environment variables for your session
$env:AZURE_SPEECH_KEY = "your-speech-key-here"
$env:AZURE_SPEECH_REGION = "westus2"
$env:AZURE_TRANSLATOR_KEY = "your-translator-key-here"
$env:AZURE_TRANSLATOR_REGION = "westus2"

# Run the application
dotnet run
```

### For Production (System Environment Variables):
1. Open System Properties → Environment Variables
2. Add the following User or System variables:
   - `AZURE_SPEECH_KEY` = your speech service key
   - `AZURE_SPEECH_REGION` = your speech service region
   - `AZURE_TRANSLATOR_KEY` = your translator service key
   - `AZURE_TRANSLATOR_REGION` = your translator service region

### For Docker:
```dockerfile
ENV AZURE_SPEECH_KEY=your-speech-key
ENV AZURE_SPEECH_REGION=westus2
ENV AZURE_TRANSLATOR_KEY=your-translator-key
ENV AZURE_TRANSLATOR_REGION=westus2
```

## 🚨 Security Best Practices

### ❌ Never Do:
- Commit API keys directly in source code
- Share keys in chat/email
- Use production keys in development

### ✅ Always Do:
- Use environment variables for secrets
- Use different keys for dev/test/prod
- Rotate keys regularly
- Use Azure Key Vault for production

## Getting Your Keys

### Azure Speech Service:
1. Go to [Azure Portal](https://portal.azure.com)
2. Navigate to your Speech Service resource
3. Go to "Keys and Endpoint"
4. Copy Key 1 and Region

### Azure Translator:
1. Go to [Azure Portal](https://portal.azure.com)  
2. Navigate to your Translator Service resource
3. Go to "Keys and Endpoint"
4. Copy Key 1 and Region