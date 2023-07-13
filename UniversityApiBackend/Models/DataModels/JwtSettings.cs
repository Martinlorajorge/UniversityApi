namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        //KEY
        public bool ValidateIssuerSigningKey { get; set; }

        public string IssuerSigningKey { get; set; } = string.Empty;


        //Isuuer
        public bool ValidateIssuer { get; set; } = true;

        public string? ValidIssuer { get; set; } 


        //Audience
        public bool ValidateAudience { get; set; } = true;

        public string? ValidAudience { get; set; }


        //Expiration
        public bool RequireExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; } = true;


    }
}
