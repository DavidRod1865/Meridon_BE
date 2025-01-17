using Supabase;

namespace CTRMBackend.Services
{
    public class SupabaseClientService
    {
        private readonly Client _client;

        public SupabaseClientService()
        {
            var supabaseURL = "https://stljtmhgmaeqqeeafirm.supabase.co";
            if (string.IsNullOrEmpty(supabaseURL))
            {
                throw new InvalidOperationException("Supabase URL is not set in environment variables.");
            }

            var apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InN0bGp0bWhnbWFlcXFlZWFmaXJtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzcwNzY5NDAsImV4cCI6MjA1MjY1Mjk0MH0.nOgr0cDYu7tEsffpkyK-GS-tkcBYBUoDb1Opgz4Msbw";
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Supabase API key is not set in environment variables.");
            }

            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _client = new Client(supabaseURL, apiKey, options);
            _client.InitializeAsync().Wait();
        }

        public Client GetClient()
        {
            return _client;
        }
    }
}
