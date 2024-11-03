public class UserSession
{
    private static UserSession _instance;
    private static readonly object _lock = new object();

    public int MemberId { get; private set; }
    public string Email { get; private set; }
    public int RoleId { get; private set; }
    public bool IsLoggedIn { get; private set; }

    public UserSession() { }

    public static UserSession GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
            }
        }
        return _instance;
    }

    public void Login(int memberId, string email, int roleId)
    {
        MemberId = memberId;
        Email = email;
        RoleId = roleId;
        IsLoggedIn = true;
    }

    public void Logout()
    {
        MemberId = 0;
        Email = null;
        RoleId = 0;
        IsLoggedIn = false;
    }
}
