using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EInvoiceAndEReceipt.Data.DTOs;
using EInvoiceAndEReceipt.Data.Entities;
using AutoMapper;
using EInvoiceAndEReceipt.Application.Generics;

namespace EInvoiceAndEReceipt.Data.Configuration
{
    public class MappingConfig:Profile
    {
       public MappingConfig()
       {
            CreateMap<DocumentDTO, Invoice>().ReverseMap();
            CreateMap<DocumentDTO, Invoice>()
    .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => "i"))
    .ForMember(dest => dest.DocumentTypeVersion, opt => opt.MapFrom(src => "1.0"));

            CreateMap<AcceptedInvoice, DocumentDTO>().ReverseMap();
            CreateMap<RejectedInvoice, DocumentDTO>().ReverseMap();


        CreateMap<IssuerDTO, Issuer>().ReverseMap();
        CreateMap<RecieverDTO, Receiver>().ReverseMap();
        CreateMap<IssuerAddressDTO, IssuerAddress>().ReverseMap();
        CreateMap<RecieverAddressDTO, ReceiverAddress>().ReverseMap();
        CreateMap<PaymentDTO, Payment>().ReverseMap();
        CreateMap<DelievryDTO, Delievry>().ReverseMap();

        
       }
    }
}