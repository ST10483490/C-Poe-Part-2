using System;
using System.Collections.Generic;

namespace ChatBotGUI
{
    /// <summary>
    /// Manages conversation state, user topic memory, keyword recognition, 
    /// sentiment analysis, and randomized response generation.
    /// </summary>
    public class ChatEngine
    {
        private string userName = "";
        private string lastTopic = "";
        private string favoriteTopic = ""; // Stores user's remembered interest
        private Random random = new Random();

        public bool IsNameRegistered => !string.IsNullOrWhiteSpace(userName);

        public string SetUserName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Name cannot be empty. Please enter your name:";

            userName = input.Trim();
            return $"Hello, {userName}! Welcome to your Cybersecurity Awareness GUI Assistant.\n" +
                   $"You can ask me about 'password', 'scam', 'privacy', or tell me what topic you are interested in!";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type a question or topic so I can assist you!";

            string lowerInput = input.ToLower();

            // 1. Detect Sentiment Prefix
            string sentimentPrefix = DetectSentiment(lowerInput);

            // 2. Memory & Topic Interest Storing (Section 5)
            if (lowerInput.Contains("interested in") || lowerInput.Contains("favorite") || lowerInput.Contains("like to learn about"))
            {
                if (lowerInput.Contains("password")) favoriteTopic = "password";
                else if (lowerInput.Contains("scam") || lowerInput.Contains("phishing")) favoriteTopic = "scam";
                else if (lowerInput.Contains("privacy") || lowerInput.Contains("data")) favoriteTopic = "privacy";

                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    lastTopic = favoriteTopic;
                    return $"{sentimentPrefix}Great! I'll remember that you're interested in {favoriteTopic} safety, {userName}. It's a crucial part of staying safe online!\n\n" +
                           GetTopicGuidance(favoriteTopic);
                }
            }

            // 3. Keyword Recognition & Immediate Guidance (Section 2 & 6)
            if (lowerInput.Contains("exit") || lowerInput.Contains("quit") || lowerInput.Contains("bye"))
            {
                return $"{sentimentPrefix}Goodbye, {userName}! Stay safe online!";
            }
            if (lowerInput.Contains("help") || lowerInput.Contains("topics"))
            {
                string memoryNote = string.IsNullOrEmpty(favoriteTopic)
                    ? ""
                    : $"\n(Remembered Interest: {favoriteTopic.ToUpper()})";

                return $"{sentimentPrefix}Here are topics you can explore, {userName}:\n" +
                       $"• Password Safety\n• Scam & Phishing Defense\n• Data Privacy{memoryNote}\n" +
                       $"(You can also ask follow-up questions or tell me what you're interested in!)";
            }
            if (lowerInput.Contains("password"))
            {
                lastTopic = "password";
                return sentimentPrefix + GetTopicGuidance("password");
            }
            if (lowerInput.Contains("scam") || lowerInput.Contains("phishing"))
            {
                lastTopic = "scam";
                return sentimentPrefix + GetTopicGuidance("scam");
            }
            if (lowerInput.Contains("privacy") || lowerInput.Contains("data"))
            {
                lastTopic = "privacy";
                return sentimentPrefix + GetTopicGuidance("privacy");
            }

            // 4. Conversation Flow & Follow-ups (Section 4 & 5 Memory Recall)
            if (lowerInput.Contains("more") || lowerInput.Contains("detail") || lowerInput.Contains("tell me more") || lowerInput.Contains("another tip"))
            {
                string activeTopic = !string.IsNullOrEmpty(lastTopic) ? lastTopic : favoriteTopic;

                if (activeTopic == "password")
                {
                    return $"{sentimentPrefix}Following up on passwords, {userName}: Make sure you change default router and device passwords immediately!";
                }
                if (activeTopic == "scam")
                {
                    return $"{sentimentPrefix}Regarding scams, {userName}: Cybercriminals often use urgent financial threats or fake prizes to trick victims.";
                }
                if (activeTopic == "privacy")
                {
                    return $"{sentimentPrefix}As someone focused on privacy, {userName}, you might also want to clear your browser cookies regularly and review account security settings.";
                }

                return $"{sentimentPrefix}What specific topic would you like more details on, {userName}?";
            }

            // 5. Fallback Responses
            return sentimentPrefix + GetRandomResponse(new List<string> {
                $"I didn't quite catch that, {userName}. Try asking about 'password', 'scam', or 'privacy'.",
                $"Hmm, I'm focused on cybersecurity! Feel free to ask about passwords, scams, or privacy.",
                $"Not sure I understand, {userName}. Type 'help' to see what topics we can cover."
            });
        }

        private string GetTopicGuidance(string topic)
        {
            if (topic == "password")
            {
                return $"[Password Guidance for {userName}]:\n" +
                       GetRandomResponse(new List<string> {
                           "1. Use strong passwords at least 12-16 characters long.\n2. Mix letters, numbers, and symbols.\n3. Never reuse passwords across sites.",
                           "1. Always use a trusted password manager.\n2. Enable Multi-Factor Authentication (MFA).\n3. Use a unique password for each account."
                       });
            }
            if (topic == "scam")
            {
                return $"[Scam Defense Guidance for {userName}]:\n" +
                       GetRandomResponse(new List<string> {
                           "1. Check sender email addresses carefully for subtle typos.\n2. Be suspicious of unexpected messages demanding urgent action.\n3. Never click unverified links.",
                           "1. Verify requests for sensitive info through official channels.\n2. Do not open unknown email attachments.\n3. Scammers often disguise themselves as trusted organisations."
                       });
            }
            if (topic == "privacy")
            {
                return $"[Privacy Guidance for {userName}]:\n" +
                       GetRandomResponse(new List<string> {
                           "1. Review app permissions and limit what data you share online.\n2. Use privacy-focused web browsers and search engines.\n3. Keep your software updated.",
                           "1. Be cautious sharing personal details on public social media.\n2. Use encrypted messaging tools for sensitive conversations.\n3. Turn off location tracking for unused apps."
                       });
            }
            return "";
        }

        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("hack"))
            {
                return "It's completely understandable to feel that way. Cybersecurity can feel overwhelming, but staying informed is the best defense! ";
            }
            if (input.Contains("confused") || input.Contains("stuck") || input.Contains("hard") || input.Contains("frustrated"))
            {
                return "Don't worry, cybersecurity concepts can be tricky, but I'm here to simplify things for you! ";
            }
            if (input.Contains("curious") || input.Contains("want to learn") || input.Contains("interested"))
            {
                return "It's awesome that you're taking proactive steps to stay safe online! ";
            }
            if (input.Contains("thank") || input.Contains("great") || input.Contains("awesome") || input.Contains("good"))
            {
                return "I'm glad I can help! ";
            }
            return "";
        }

        private string GetRandomResponse(List<string> options)
        {
            int index = random.Next(options.Count);
            return options[index];
        }
    }
}
