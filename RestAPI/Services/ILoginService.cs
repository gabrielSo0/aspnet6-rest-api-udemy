﻿using RestAPI.Data.VO;

namespace RestAPI.Services
{
    public interface ILoginService
    {
        TokenVO ReturnUserToken(UserVO user);
    }
}
