﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Notary.Contract;

namespace Notary.Model
{
    public sealed class CertificateModel : BaseModel
    {
        public CertificateModel()
        {

        }

        public CertificateModel(Certificate contract) : base(contract)
        {
            Data = contract.Data;
            IssuingSlug = contract.IssuingSlug;
            IsCaCertificate = contract.IsCaCertificate;
            KeySlug = contract.KeySlug;
            KeyUsages = contract.KeyUsages;
            Issuer = new DistinguishedNameModel(contract.Issuer);
            Name = contract.Name;
            NotAfter = contract.NotAfter;
            NotBefore = contract.NotBefore;
            RevocationDate = contract.RevocationDate;
            SerialNumber = contract.SerialNumber;
            SignatureAlgorithm = contract.SignatureAlgorithm;
            Subject = new DistinguishedNameModel(contract.Subject);
            SubjectAlternativeNames = contract.SubjectAlternativeNames?.Select(s => new SanModel(s)).ToList();
            Thumbprint = contract.Thumbprint;
        }

        [BsonElement("data"), BsonRequired]
        public string Data { get; set; }

        [BsonElement("is_ca"), BsonRequired]
        public bool IsCaCertificate { get; set; }

        [BsonElement("iss"), BsonRequired]
        public DistinguishedNameModel Issuer { get; set; }

        [BsonElement("iss_slug"), BsonRequired]
        public string IssuingSlug { get; set; }

        [BsonElement("key_usage"), BsonRequired]
        public List<string> KeyUsages { get; set; }

        [BsonElement("key_slug"), BsonRequired]
        public string KeySlug { get; set; }

        [BsonElement("name"), BsonRequired]
        public string Name { get; set; }

        [BsonElement("nb"), BsonRequired]
        public DateTime NotBefore { get; set; }

        [BsonElement("na"), BsonRequired]
        public DateTime NotAfter { get; set; }

        [BsonElement("rd"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public DateTime? RevocationDate { get; set; }

        [BsonElement("sn"), BsonRequired]
        public string SerialNumber { get; set; }

        [BsonElement("sig_alg"), BsonRequired]
        public string SignatureAlgorithm { get; set; }

        [BsonElement("sub"), BsonRequired]
        public DistinguishedNameModel Subject { get; set; }

        [BsonElement("san"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public List<SanModel> SubjectAlternativeNames { get; set; }

        [BsonElement("thumb"), BsonRequired]
        public string Thumbprint { get; set; }
    }
}
