using AutoMapper;



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



         

        });

        var mapper = new Mapper(config);

        return mapper;

    }

}
