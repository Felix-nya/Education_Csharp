using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal enum OrderStatus
    {
        New,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
    internal class Order
    {
        internal int Id { get; set; }
        internal string ProductName { get; set; } = string.Empty;
        internal OrderStatus Status { get; set; }
        internal void DisplayOrderInfo() => Console.WriteLine($"ID: {Id}, наименование продукта: {ProductName}. Статус: {Status}");
        internal bool CanCancelOrder() => Status == OrderStatus.New || Status == OrderStatus.Processing;
    }

    [Flags]
    internal enum UserPermissions
    {
        None = 0,
        Read = 1,
        Write = 2,
        Delete = 4,
        Execute = 8,
        ReadWrite = Read | Write,
        ReadWriteDelete = Delete | ReadWrite,
        ReadWriteDeleteExecute = Execute | ReadWriteDelete
    }
    internal class USER
    {
        internal string Name { get; set; } = string.Empty;
        internal UserPermissions Permissions { get; set; }
        internal bool HasPermission(UserPermissions perm) => Permissions >= perm;
        internal void AddPermission(UserPermissions perm) => Permissions = Enum.IsDefined(typeof(UserPermissions),
            (UserPermissions)((int)perm + Permissions)) ? (UserPermissions)((int)perm + Permissions) : perm;
        internal void RemovePermission(UserPermissions perm) => Permissions = Enum.IsDefined(typeof(UserPermissions),
            (UserPermissions)(-(int)perm + Permissions)) ? (UserPermissions)(-(int)perm + Permissions) : Permissions;
    }

    internal enum TextCommand
    {
        Unknown,
        Start,
        Stop,
        Pause,
        Resume
    }

    internal enum NotificationType
    {
        Email,
        SMS,
        Push,
        ALL
    }
    internal enum Priority
    {
        Low,
        Medium,
        High
    }
    internal class Notification
    {
        internal string Message { get; } = String.Empty;
        internal NotificationType Type { get; }
        internal Priority Priority { get; }
        internal DateTime CreateAt { get; }
        internal Notification(string mes, NotificationType ty, Priority pr)
        {
            Message = mes;
            Type = ty;
            Priority = pr;
            CreateAt = DateTime.Now;
        }
        internal static Notification CreateUrgentNotification(string mes, NotificationType ty)
        {
            return new(mes, ty, Priority.High);
        }
        internal void Send()
        {
            if (!string.IsNullOrEmpty(Message) && Type!=NotificationType.ALL) Console.WriteLine($"{Message}, {Type}, {CreateAt}");
            if (Type == NotificationType.ALL) Console.WriteLine($"{Message}, {NotificationType.SMS} {NotificationType.Email} {NotificationType.Push}, {CreateAt}");
        }
        internal bool CanSend()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                if (CreateAt.Hour >= 9 && CreateAt.Hour <= 18 && Priority == Priority.Low) return true;
                else if (Priority == Priority.High || Priority == Priority.Medium) return true;
                return false;
            }
            return false;
        }
    }
}
