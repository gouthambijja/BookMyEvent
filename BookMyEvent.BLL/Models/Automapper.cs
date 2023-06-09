using AutoMapper;
using BookMyEvent.BLL.Models;
using db.Models;

public class Automapper

{

    public static Mapper InitializeAutomapper()

    {

        var config = new MapperConfiguration(cfg =>

        {

            //cfg.CreateMap<User, UserBL>();

            //cfg.CreateMap<UserBL, User>();

            //cfg.CreateMap<UserBL, LoginUserModel>();

            //cfg.CreateMap<Transaction, TransactionBL>();

            //cfg.CreateMap<TransactionBL, Transaction>();
            cfg.CreateMap<BLUser, User>().ReverseMap();
            cfg.CreateMap<BLAdministrator, Administration>().ReverseMap();
            cfg.CreateMap<BLUserInputForm, UserInputForm>().ReverseMap();
            cfg.CreateMap<BLUserInputFormField, UserInputFormField>().ReverseMap();
        });

        var mapper = new Mapper(config);

        return mapper;

    }

}
