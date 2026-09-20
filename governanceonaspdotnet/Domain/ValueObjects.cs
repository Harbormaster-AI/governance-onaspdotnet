using System.ComponentModel.DataAnnotations.Schema;

namespace governanceonaspdotnet.Domain;

    [ComplexType]
    public record EmailAddress(
    string Value
    );

    [ComplexType]
    public record URL(
    string Value
    );

