using System;

namespace DrinkVendingMachine.Aspects
{
    /// <summary>
    /// Для пометки метода, который должен выполняться в рамках транзакции БД. Класс с такими методами
    /// также должен быть помечен данным атрибутом
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TransactionAttribute : Attribute 
    {

    }
}
