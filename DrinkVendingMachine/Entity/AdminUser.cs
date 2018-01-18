namespace DrinkVendingMachine.Entity
{
    /// <summary>
    /// Пользователь - администратор
    /// </summary>
    public class AdminUser : BaseEntity
    {
        
        /// <summary>
        /// Логин
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}