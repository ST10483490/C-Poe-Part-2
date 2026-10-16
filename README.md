# Technologies & EnvironmentLanguage & Framework: C# on .NET 8.0 Windows Forms (WindowsForms).
Audio Handling: System.Media.SoundPlayer combined with System.IO for file path validation and dynamic base directory resolution.
# Object-Oriented Programming (OOP) ArchitectureThe application is structured into four modular, clean classes to ensure maintainability and separation of concerns.
# Program.cs: Handles application entry point execution, visual styles initialization, and runs the main form.
#MainForm.cs: Manages the graphical user interface layout, user input events (KeyDown and button clicks), and chat display rendering.
# ChatEngine.cs: Encapsulates all conversational logic, state management, memory, keyword processing, and sentiment analysis.
#AudioPlayer.cs: Static utility class dedicated to safely loading and playing audio greetings with error handling.
# Core Features ImplementedGUI Design & ASCII Art Header:Custom dark-themed user interface (Color.FromArgb) utilizing a monospaced Consolas font for precise ASCII alignment of the National Cybersecurity Awareness Assistance.
# Styled interactive components including a RichTextBox chat history log, a user TextBox, and a styled flat Button.
# Voice Implementation:Integrated an audio greeting playback (Audio Greeting.wav) triggered upon application startup via SoundPlayer.
# Keyword Recognition:Detects primary cybersecurity topics in user input, specifically targeting "password", "scam" / "phishing", and "privacy" / "data", and delivers targeted educational guidance.
Randomized Responses:Utilizes System.Random alongside List<string> data structures to dynamically pull varied informative tips and fallback messages, preventing repetitive chatbot interactions.
# Memory and Recall:Stores user state variables including userName, lastTopic, and favoriteTopic to dynamically reference user interests and personalize responses later in the conversation.
# Sentiment Detection & Empathetic Flow:Analyzes user tone for keywords reflecting emotion (such as "worried", "scared", "confused", or "curious") and adapts the chatbot's response prefix to offer immediate support and direct guidance without requiring extra prompts.
Continuous Conversation Flow:Tracks active conversation context so users can ask follow-up questions like "tell me more" or "give me another tip" without restarting the session.  
