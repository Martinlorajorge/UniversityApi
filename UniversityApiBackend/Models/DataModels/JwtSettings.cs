namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        //KEY
        public bool ValidateIssuerSigningKey { get; set; }

        public string? IssuerSiningKey { get; set; }


        //Isuuer
        public bool ValidateIssuer { get; set; } = true;

        public string? ValidIssuer { get; set; }


        //Audience
        public bool ValidateAudience { get; set; } = true;

        public string? ValidAudience { get; set; }


        //Expiration
        public bool ExpirationTime { get; set; }

        public bool ValidateLifetime { get; set; } = true;


    }
}
