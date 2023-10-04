using System;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace TMS.Common.Cryptography.X509
{
    /// <summary>
    /// 
    /// </summary>
    public static class CertificateGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signatureAlgorithm"></param>
        /// <param name="subject"></param>
        /// <param name="issuer"></param>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        public static X509Certificate CreateX509V1Certificate(string signatureAlgorithm, string subject, string issuer, int keyLength)
        {
            var random = new SecureRandom(new CryptoApiRandomGenerator());
            var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);

            var certificateGenerator = new X509V1CertificateGenerator();
            certificateGenerator.SetSerialNumber(serialNumber);
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);
            certificateGenerator.SetSubjectDN(new X509Name(subject));
            certificateGenerator.SetIssuerDN(new X509Name(issuer));

            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(1);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(new KeyGenerationParameters(random, keyLength));
            var subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            return certificateGenerator.Generate(subjectKeyPair.Private, random);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signatureAlgorithm"></param>
        /// <param name="subject"></param>
        /// <param name="issuer"></param>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        public static X509Certificate CreateX509V3Certificate(string signatureAlgorithm, string subject, string issuer, int keyLength)
        {
            var random = new SecureRandom(new CryptoApiRandomGenerator());
            var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);

            var certificateGenerator = new X509V3CertificateGenerator();
            certificateGenerator.SetSerialNumber(serialNumber);
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);
            certificateGenerator.SetSubjectDN(new X509Name(subject));
            certificateGenerator.SetIssuerDN(new X509Name(issuer));

            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(1);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(new KeyGenerationParameters(random, keyLength));
            var subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            return certificateGenerator.Generate(subjectKeyPair.Private, random);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="certificate"></param>
        public static void SaveCertificate(string file, X509Certificate certificate)
        {
            using (var output = new StreamWriter(file))
            {
                var pemWriter = new PemWriter(output);
                pemWriter.WriteObject(certificate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static X509Certificate LoadCertificate(string file)
        {
            using (var input = new StreamReader(file))
            {
                var pemReader = new PemReader(input);
                return pemReader.ReadObject() as X509Certificate;
            }
        }
    }
}
