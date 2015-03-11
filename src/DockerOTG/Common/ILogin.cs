using DockerOTG.Model;

namespace DockerOTG.Common
{
    public interface ILogin
    {
        bool LoginSuccessful(LoginViewModel model);
    }
}