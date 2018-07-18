using AutoMapper;
using homework_5_bsa2018.BLL;


namespace homework_5_bsa2018.BLL
{
    public class MapperInitializator
    {
        private static bool flag = true;

        public void Initialize()
        {
            if (flag)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new MapperProfile()));
            }
            flag = false;
        }
    }
}
