using AutoMapper;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Models;
using db.Models;

namespace BookMyEvent.BLL.Utilities;
public static class Automapper

{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BLEvent, Event>().ReverseMap();
            cfg.CreateMap<BLTransaction, Transaction>().ReverseMap();
            cfg.CreateMap<BLUser, User>().ReverseMap();
            cfg.CreateMap<BLAdministrator, Administration>().ReverseMap();
            cfg.CreateMap<BLForm, Form>().ReverseMap();
            cfg.CreateMap<BLRegistrationFormFields, RegistrationFormField>().ReverseMap();
            cfg.CreateMap<BLEventImages, EventImage>().ReverseMap();
            cfg.CreateMap<BLUserInputForm, UserInputForm>().ReverseMap();
            cfg.CreateMap<BLUserInputFormField, UserInputFormField>().ReverseMap();
            cfg.CreateMap<BLTicket, Ticket>().ReverseMap();
            cfg.CreateMap<BLOrganisation, Organisation>().ReverseMap();
            cfg.CreateMap<BLEventCategory, EventCategory>().ReverseMap();
            cfg.CreateMap<BLFieldType, FieldType>().ReverseMap();
            cfg.CreateMap<BLFileType, FileType>().ReverseMap();
        });
        var mapper = new Mapper(config);
        return mapper;
    }
}
