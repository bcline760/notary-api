﻿namespace Notary.Test;

using Notary.Interface.Service;
using Notary.Contract;
using Notary.Interface.Repository;
using Notary.Service;

public class CertificateAuthorityServiceTest : NotaryTest
{
    private Mock<ICertificateAuthorityRepository> _caRepo;
    private Mock<ICertificateRepository> _certificateRepo;
    private Mock<ILog> _log;
    private CertificateAuthorityService _service = null;

    public CertificateAuthorityServiceTest()
    {
        _caRepo = new Mock<ICertificateAuthorityRepository>();
        _certificateRepo = new Mock<ICertificateRepository>();
        _log = new Mock<ILog>();
    }

    [SetUp]
    public async Task Setup()
    {
        var ca = MockCa();
        var certificate = CreateCertificateMock();
        var config = MockConfiguration();
        var caList = new List<CertificateAuthority>()
        {
            ca
        };
        var certificateList = new List<Certificate>()
        {
            certificate
        };

        _caRepo.Setup(r => r.SaveAsync(ca));
        _caRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(caList);
        _caRepo.Setup(r => r.GetAsync("string")).ReturnsAsync(ca);

        _certificateRepo.Setup(r => r.GetCertificatesByCaAsync("string")).ReturnsAsync(certificateList);

        _service = new CertificateAuthorityService(
            _caRepo.Object,
            _certificateRepo.Object,
            config,
            _log.Object
        );
    }

    [Test]
    public async Task GetAllAuthoritiesAndCertificateTest()
    {
        var ca = MockCa();
        var certificate = CreateCertificateMock();
        var ccl = new CaCertificateList
        {
            Name = ca.Name,
            ParentCaSlug = ca.ParentCaSlug,
            Slug = ca.Slug,
            CertificateCollection = new List<Certificate>() { certificate }
        };
        var expected = new List<CaCertificateList>() { ccl };
        var actual = await _service.GetAllAuthoritiesAndCertificate();

        Assert.That(actual != null, "The actual returned is null");
        Assert.That(actual.Count > 0, "The actual is empty");
        Assert.That(expected.Count == actual.Count, "Count differs between actual and expected.");
        Assert.That(expected[0].Slug == actual[0].Slug);
    }

    private CertificateAuthority MockCa()
    {
        return new CertificateAuthority
        {
            Active = true,
            Created = DateTime.MinValue,
            CreatedBySlug = "string",
            DistinguishedName = new DistinguishedName
            {
                CommonName = "string",
                Country = "string",
                Locale = "string",
                Organization = "string",
                OrganizationalUnit = "string",
                StateProvince = "string"
            },
            IsIssuer = false,
            IssuingDn = new DistinguishedName
            {
                CommonName = "string",
                Country = "string",
                Locale = "string",
                Organization = "string",
                OrganizationalUnit = "string",
                StateProvince = "string"
            },
            IssuingSerialNumber = "string",
            IssuingThumbprint = "string",
            KeyAlgorithm = It.IsAny<Algorithm>(),
            KeyCurve = null,
            KeyLength = 0,
            Name = "string",
            ParentCaSlug = "string",
            Slug = "string",
            Updated = null,
            UpdatedBySlug = "string"
        };
    }

    private Certificate CreateCertificateMock()
    {
        return new Certificate
        {
            Active = true,
            CertificateAuthoritySlug = "string",
            CreatedBySlug = "string",
            Created = DateTime.MinValue,
            IsCaCertificate = false,
            Issuer = new DistinguishedName
            {
                CommonName = "string",
                Country = "string",
                Locale = "string",
                Organization = "string",
                OrganizationalUnit = "string",
                StateProvince = "string"
            },
            KeyAlgorithm = Algorithm.RSA,
            KeyCurve = null,
            KeyLength = 0,
            KeyUsage = 0,
            Name = "string",
            NotAfter = DateTime.MaxValue,
            NotBefore = DateTime.MinValue,
            RevocationDate = null,
            SerialNumber = "string",
            SignatureAlgorithm = "string",
            Slug = "string",
            Subject = new DistinguishedName
            {
                CommonName = "string",
                Country = "string",
                Locale = "string",
                Organization = "string",
                OrganizationalUnit = "string",
                StateProvince = "string"
            },
            SubjectAlternativeNames = new List<SubjectAlternativeName>(),
            Thumbprint = "string",
            Updated = null,
            UpdatedBySlug = null
        };
    }
}
