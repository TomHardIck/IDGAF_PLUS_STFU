using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class PraktikaSevContext : DbContext
    {
        public PraktikaSevContext()
        {
        }

        public PraktikaSevContext(DbContextOptions<PraktikaSevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Dealer> Dealers { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; } = null!;
        public virtual DbSet<DeliveryType> DeliveryTypes { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<DiscountDatum> DiscountData { get; set; } = null!;
        public virtual DbSet<DiscountPercent> DiscountPercents { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDatum> OrderData { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<PositionDatum> PositionData { get; set; } = null!;
        public virtual DbSet<PositionsInOrder> PositionsInOrders { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UsersDatum> UsersData { get; set; } = null!;
        public virtual DbSet<UsersDiscount> UsersDiscounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Category_Name");
            });

            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.HasKey(e => e.IdDealer);

                entity.ToTable("Dealer");

                entity.Property(e => e.IdDealer).HasColumnName("ID_Dealer");

                entity.Property(e => e.DealerAddr)
                    .IsUnicode(false)
                    .HasColumnName("Dealer_Addr");

                entity.Property(e => e.DealerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Dealer_Name");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => e.IdDelivery);

                entity.ToTable("Delivery");

                entity.Property(e => e.IdDelivery).HasColumnName("ID_Delivery");

                entity.Property(e => e.DeliveryCost).HasColumnName("Delivery_Cost");

                entity.Property(e => e.DeliveryStatusId).HasColumnName("Delivery_Status_ID");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("Delivery_Type_ID");
            });

            modelBuilder.Entity<DeliveryStatus>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryStatus);

                entity.ToTable("Delivery_Status");

                entity.Property(e => e.IdDeliveryStatus).HasColumnName("ID_Delivery_Status");

                entity.Property(e => e.DeliveryStatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Delivery_Status_Name");
            });

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.HasKey(e => e.IdDeliveryType);

                entity.ToTable("Delivery_Type");

                entity.Property(e => e.IdDeliveryType).HasColumnName("ID_Delivery_Type");

                entity.Property(e => e.DeliveryTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Delivery_Type_Name");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.IdDiscount);

                entity.ToTable("Discount");

                entity.Property(e => e.IdDiscount).HasColumnName("ID_Discount");

                entity.Property(e => e.DiscountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Discount_Name");

                entity.Property(e => e.DiscountPercentId).HasColumnName("DiscountPercent_ID");
            });

            modelBuilder.Entity<DiscountDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DiscountData");

                entity.Property(e => e.ЛогинПользователя)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Логин пользователя");

                entity.Property(e => e.ПроцентСкидки)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Процент скидки");

                entity.Property(e => e.ТипСкидки)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Тип скидки");
            });

            modelBuilder.Entity<DiscountPercent>(entity =>
            {
                entity.HasKey(e => e.IdDiscountPercent);

                entity.ToTable("DiscountPercent");

                entity.Property(e => e.IdDiscountPercent).HasColumnName("ID_DiscountPercent");

                entity.Property(e => e.DiscountPercentValue)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Discount_Percent_Value");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => e.IdFavorites);

                entity.Property(e => e.IdFavorites).HasColumnName("ID_Favorites");

                entity.Property(e => e.PositionId).HasColumnName("Position_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.DeliveryId).HasColumnName("Delivery_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Order_Num");

                entity.Property(e => e.OrderSum).HasColumnName("Order_Sum");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<OrderDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderData");

                entity.Property(e => e.ДатаЗаказа)
                    .HasColumnType("date")
                    .HasColumnName("Дата заказа");

                entity.Property(e => e.НомерЗаказа)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Номер заказа");

                entity.Property(e => e.СтатусДоставки)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Статус доставки");

                entity.Property(e => e.СтатусЗаказа)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Статус заказа");

                entity.Property(e => e.СуммаЗаказа).HasColumnName("Сумма заказа");

                entity.Property(e => e.ТипДоставки)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Тип доставки");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.IdPosition);

                entity.ToTable("Position");

                entity.Property(e => e.IdPosition).HasColumnName("ID_Position");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.Property(e => e.DealerId).HasColumnName("Dealer_ID");

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Photo_URL");

                entity.Property(e => e.PositionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Position_Name");

                entity.Property(e => e.PositionPrice).HasColumnName("Position_Price");

                entity.Property(e => e.PositionQuantity).HasColumnName("Position_Quantity");

                entity.Property(e => e.PositionSlife)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Position_SLife");
            });

            modelBuilder.Entity<PositionDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PositionData");

                entity.Property(e => e.АдресПоставщикаТовара)
                    .IsUnicode(false)
                    .HasColumnName("Адрес поставщика товара");

                entity.Property(e => e.ДоКакойДатыГоден)
                    .HasColumnType("date")
                    .HasColumnName("До какой даты годен");

                entity.Property(e => e.КоличествоТовара).HasColumnName("Количество товара");

                entity.Property(e => e.НазваниеКатегорииТовара)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Название категории товара");

                entity.Property(e => e.НазваниеТовара)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Название товара");

                entity.Property(e => e.ПоставщикТовара)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Поставщик товара");

                entity.Property(e => e.СсылкаНаФото)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ссылка на фото");

                entity.Property(e => e.ЦенаТовара).HasColumnName("Цена товара");
            });

            modelBuilder.Entity<PositionsInOrder>(entity =>
            {
                entity.HasKey(e => e.IdPositionInOrder);

                entity.ToTable("Positions_In_Order");

                entity.Property(e => e.IdPositionInOrder).HasColumnName("ID_Position_In_Order");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.PositionId).HasColumnName("Position_ID");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus);

                entity.ToTable("Status");

                entity.Property(e => e.IdStatus).HasColumnName("ID_Status");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Status_Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Salt)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserFname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_FName");

                entity.Property(e => e.UserLname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_LName");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Login");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("User_Password");

                entity.Property(e => e.UserRoleId).HasColumnName("User_Role_ID");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRole)
                    .HasName("PK_User_Role");

                entity.ToTable("UserRole");

                entity.Property(e => e.IdUserRole).HasColumnName("ID_User_Role");

                entity.Property(e => e.UserRoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_Role_Name");
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UsersData");

                entity.Property(e => e.Имя)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Логин)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Пароль)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.РольПользователя)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Роль пользователя");

                entity.Property(e => e.Фамилия)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersDiscount>(entity =>
            {
                entity.HasKey(e => e.IdUserDiscount);

                entity.ToTable("UsersDiscount");

                entity.Property(e => e.IdUserDiscount).HasColumnName("ID_UserDiscount");

                entity.Property(e => e.DiscountId).HasColumnName("Discount_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
