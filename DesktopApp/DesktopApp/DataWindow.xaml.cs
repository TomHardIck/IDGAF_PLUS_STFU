using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using API.Models;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DesktopApp
{
    /// <summary>
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        HttpClient client = new HttpClient();
        private static object needToDelete;
        public DataWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://192.168.56.1:7187/api/");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            UpdateGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            {
            }
        }

        private async void UpdateGrid()
        {
            catGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Categories")))["data"].ToObject<List<Category>>();
            

            dealGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Dealers")))["data"].ToObject<List<Dealer>>();
            

            statusDelGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DeliveryStatus")))["data"].ToObject<List<DeliveryStatus>>();
            

            delTypeGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DeliveryTypes")))["data"].ToObject<List<DeliveryType>>();
            

            deliveryGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Deliveries")))["data"].ToObject<List<Delivery>>();
            

            statusGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Status")))["data"].ToObject<List<Status>>();
            

            rolesGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("UserRoles")))["data"].ToObject<List<UserRole>>();
            

            discountGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Discounts")))["data"].ToObject<List<Discount>>();
            

            discountPerc.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DiscountPercents")))["data"].ToObject<List<DiscountPercent>>();
            

            favGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Favorites")))["data"].ToObject<List<Favorite>>();
            

            usersGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Users")))["data"].ToObject<List<User>>();


            posGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Positions")))["data"].ToObject<List<Position>>();


            orderGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Orders")))["data"].ToObject<List<Order>>();


            posInOrdGrid.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("PositionsInOrders")))["data"].ToObject<List<PositionsInOrder>>();

            discUsers.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("UsersDiscounts")))["data"].ToObject<List<UsersDiscount>>();


            idTypeDel.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DeliveryTypes")))["data"].ToObject<List<DeliveryType>>().Select(x => x.DeliveryTypeName);
            idStatusDel.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DeliveryStatus")))["data"].ToObject<List<DeliveryStatus>>().Select(x => x.DeliveryStatusName);
            idDiscPerc.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("DiscountPercents")))["data"].ToObject<List<DiscountPercent>>().Select(x => x.DiscountPercentValue);
            idUser.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Users")))["data"].ToObject<List<User>>().Select(x => x.UserLogin);
            idUserOrd.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Users")))["data"].ToObject<List<User>>().Select(x => x.UserLogin);
            idPos.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Positions")))["data"].ToObject<List<Position>>().Select(x => x.PositionName);
            idPosInPosOrd.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Positions")))["data"].ToObject<List<Position>>().Select(x => x.PositionName);
            idOrdInPosOrd.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Orders")))["data"].ToObject<List<Order>>().Select(x => x.OrderNum);
            idDelivery.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Deliveries")))["data"].ToObject<List<Delivery>>().Select(x => x.DeliveryCost);
            idStatus.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Status")))["data"].ToObject<List<Status>>().Select(x => x.StatusName);
            idCat.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Categories")))["data"].ToObject<List<Category>>().Select(x => x.CategoryName);
            idDealer.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Dealers")))["data"].ToObject<List<Dealer>>().Select(x => x.DealerName);
            role.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("UserRoles")))["data"].ToObject<List<UserRole>>().Select(x => x.UserRoleName);
            idUserDisc.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Users")))["data"].ToObject<List<User>>().Select(x => x.UserLogin);
            idDisc.ItemsSource = ((JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Discounts")))["data"].ToObject<List<Discount>>().Select(x => x.DiscountName);
        }

        private void catGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Category selectedRow = (Category)catGrid.SelectedItems[0];
                catName.Text = selectedRow.CategoryName;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    await client.PostAsJsonAsync("Categories", new Category { CategoryName = catName.Text});
                    UpdateGrid();
                    break;
                case 1:
                    await client.PostAsJsonAsync("Dealers", new Dealer {DealerAddr = dealAddr.Text, DealerName = dealName.Text });
                    UpdateGrid();
                    break;
                case 2:
                    Delivery delivery = new Delivery { DeliveryCost = int.Parse(costDel.Text) };
                    foreach(DeliveryStatus status in statusDelGrid.ItemsSource)
                    {
                        if(status.DeliveryStatusName == idStatusDel.Text)
                        {
                            delivery.DeliveryStatusId = status.IdDeliveryStatus;
                        }
                    }
                    foreach (DeliveryType type in delTypeGrid.ItemsSource)
                    {
                        if (type.DeliveryTypeName == idTypeDel.Text)
                        {
                            delivery.DeliveryTypeId = type.IdDeliveryType;
                        }
                    }
                    await client.PostAsJsonAsync("Deliveries", delivery);
                    UpdateGrid();
                    break;
                case 3:
                    await client.PostAsJsonAsync("DeliveryStatus", new DeliveryStatus { DeliveryStatusName = statusNameDel.Text });
                    UpdateGrid();
                    break;
                case 4:
                    await client.PostAsJsonAsync("DeliveryTypes", new DeliveryType { DeliveryTypeName = delTypeName.Text });
                    UpdateGrid();
                    break;
                case 5:
                    Discount discount = new Discount {DiscountName = nameDisc.Text };
                    foreach (DiscountPercent d in discountPerc.ItemsSource)
                    {
                        if (d.DiscountPercentValue == idDiscPerc.Text)
                        {
                            discount.DiscountPercentId = (int)d.IdDiscountPercent;
                        }
                    }
                    await client.PostAsJsonAsync("Discounts", discount);
                    UpdateGrid();
                    break;
                case 6:
                    await client.PostAsJsonAsync("DiscountPercents", new DiscountPercent { DiscountPercentValue = percVal.Text });
                    UpdateGrid();
                    break;
                case 7:
                    Favorite fav = new Favorite {};
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUser.Text)
                        {
                            fav.UserId = u.IdUser;
                        }
                    }
                    foreach (Position p in posGrid.ItemsSource)
                    {
                        if (p.PositionName == idPos.Text)
                        {
                            fav.PositionId = p.IdPosition;
                        }
                    }
                    await client.PostAsJsonAsync("Favorites", fav);
                    UpdateGrid();
                    break;
                case 8:
                    Order ord = new Order { OrderNum = ordNum.Text, OrderSum = double.Parse(ordSum.Text) };
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUserOrd.Text)
                        {
                            ord.UserId = u.IdUser;
                        }
                    }
                    foreach (Status s in statusGrid.ItemsSource)
                    {
                        if (s.StatusName == idStatus.Text)
                        {
                            ord.StatusId = s.IdStatus;
                        }
                    }
                    foreach (Delivery d in deliveryGrid.ItemsSource)
                    {
                        if (d.DeliveryCost.ToString() == idDelivery.Text)
                        {
                            ord.DeliveryId = d.IdDelivery;
                        }
                    }
                    await client.PostAsJsonAsync("Orders", ord);
                    UpdateGrid();
                    break;
                case 9:
                    Position pos = new Position { PositionName = posName.Text, PhotoUrl = photo.Text, PositionPrice = double.Parse(price.Text), PositionQuantity = int.Parse(posQ.Text), PositionSlife = posLife.Text, IsExists = (bool)isExists.IsChecked };
                    foreach (Dealer d in dealGrid.ItemsSource)
                    {
                        if (d.DealerName == idDealer.Text)
                        {
                            pos.DealerId = d.IdDealer;
                        }
                    }
                    foreach (Category c in catGrid.ItemsSource)
                    {
                        if (c.CategoryName == idCat.Text)
                        {
                            pos.CategoryId = c.IdCategory;
                        }
                    }
                    await client.PostAsJsonAsync("Positions", pos);
                    UpdateGrid();
                    break;
                case 10:
                    PositionsInOrder posIn = new PositionsInOrder {};
                    foreach (Position p in posGrid.ItemsSource)
                    {
                        if (p.PositionName == idPosInPosOrd.Text)
                        {
                            posIn.PositionId = p.IdPosition;
                        }
                    }
                    foreach (Order o in orderGrid.ItemsSource)
                    {
                        if (o.OrderNum == idOrdInPosOrd.Text)
                        {
                            posIn.OrderId = o.IdOrder;
                        }
                    }
                    await client.PostAsJsonAsync("PositionsInOrders", posIn);
                    UpdateGrid();
                    break;
                case 11:
                    await client.PostAsJsonAsync("Status", new Status { StatusName = statusName.Text });
                    UpdateGrid();
                    break;
                case 12:
                    User user = new User { UserLogin = login.Text, UserPassword = password.Text, UserFname = fname.Text, UserLname = lname.Text };
                    foreach (UserRole ur in rolesGrid.ItemsSource)
                    {
                        if (ur.UserRoleName == role.Text)
                        {
                            user.UserRoleId = ur.IdUserRole;
                        }
                    }
                    await client.PostAsJsonAsync("Users", user);
                    UpdateGrid();
                    break;
                case 13:
                    await client.PostAsJsonAsync("UserRoles", new UserRole { UserRoleName = roleName.Text });
                    UpdateGrid();
                    break;
                case 14:
                    UsersDiscount uDisc = new UsersDiscount {  };
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUserDisc.Text)
                        {
                            uDisc.UserId = u.IdUser;
                        }
                    }
                    foreach (Discount d in discountGrid.ItemsSource)
                    {
                        if (d.DiscountName == idDisc.Text)
                        {
                            uDisc.DiscountId = d.IdDiscount;
                        }
                    }
                    await client.PostAsJsonAsync("UsersDiscounts", uDisc);
                    UpdateGrid();
                    break;
            }
        }

        private async void editButton_Click(object sender, RoutedEventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    await client.PutAsJsonAsync("Categories/" + ((Category)needToDelete).IdCategory, new Category {IdCategory = ((Category)needToDelete).IdCategory, CategoryName = catName.Text });
                    UpdateGrid();
                    break;
                case 1:
                    await client.PutAsJsonAsync("Dealers/" + ((Dealer)needToDelete).IdDealer, new Dealer { IdDealer = ((Dealer)needToDelete).IdDealer, DealerAddr = dealAddr.Text, DealerName = dealName.Text });
                    UpdateGrid();
                    break;
                case 2:
                    Delivery delivery = new Delivery { DeliveryCost = int.Parse(costDel.Text) };
                    foreach (DeliveryStatus status in statusDelGrid.ItemsSource)
                    {
                        if (status.DeliveryStatusName == idStatusDel.Text)
                        {
                            delivery.DeliveryStatusId = status.IdDeliveryStatus;
                        }
                    }
                    foreach (DeliveryType type in delTypeGrid.ItemsSource)
                    {
                        if (type.DeliveryTypeName == idTypeDel.Text)
                        {
                            delivery.DeliveryTypeId = type.IdDeliveryType;
                        }
                    }
                    delivery.IdDelivery = ((Delivery)needToDelete).IdDelivery;
                    await client.PutAsJsonAsync("Deliveries/" + ((Delivery)needToDelete).IdDelivery, delivery);
                    UpdateGrid();
                    break;
                case 3:
                    await client.PutAsJsonAsync("DeliveryStatus/" + ((DeliveryStatus)needToDelete).IdDeliveryStatus, new DeliveryStatus { IdDeliveryStatus = ((DeliveryStatus)needToDelete).IdDeliveryStatus, DeliveryStatusName = statusNameDel.Text });
                    UpdateGrid();
                    break;
                case 4:
                    await client.PutAsJsonAsync("DeliveryTypes/" + ((DeliveryType)needToDelete).IdDeliveryType, new DeliveryType { IdDeliveryType = ((DeliveryType)needToDelete).IdDeliveryType, DeliveryTypeName = delTypeName.Text });
                    UpdateGrid();
                    break;
                case 5:
                    Discount discount = new Discount { IdDiscount = ((Discount)needToDelete).IdDiscount, DiscountName = nameDisc.Text};
                    foreach(DiscountPercent d in discountPerc.ItemsSource)
                    {
                        if(d.DiscountPercentValue == idDiscPerc.Text)
                        {
                            discount.DiscountPercentId = (int)d.IdDiscountPercent;
                        }
                    }
                    await client.PutAsJsonAsync("Discounts/" + ((Discount)needToDelete).IdDiscount, discount);
                    UpdateGrid();
                    break;
                case 6:
                    await client.PutAsJsonAsync("DiscountPercents/" + ((DiscountPercent)needToDelete).IdDiscountPercent, new DiscountPercent { IdDiscountPercent = ((DiscountPercent)needToDelete).IdDiscountPercent, DiscountPercentValue = percVal.Text });
                    UpdateGrid();
                    break;
                case 7:
                    Favorite fav = new Favorite { IdFavorites = ((Favorite)needToDelete).IdFavorites };
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUser.Text)
                        {
                            fav.UserId = u.IdUser;
                        }
                    }
                    foreach (Position p in posGrid.ItemsSource)
                    {
                        if (p.PositionName == idPos.Text)
                        {
                            fav.PositionId = p.IdPosition;
                        }
                    }
                    await client.PutAsJsonAsync("Favorites/" + ((Favorite)needToDelete).IdFavorites, fav);
                    UpdateGrid();
                    break;
                case 8:
                    Order ord = new Order { IdOrder = ((Order)needToDelete).IdOrder, OrderDate = Convert.ToDateTime(ordDate.Text), OrderNum = ordNum.Text, OrderSum = double.Parse(ordSum.Text) };
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUserOrd.Text)
                        {
                            ord.UserId = u.IdUser;
                        }
                    }
                    foreach (Status s in statusGrid.ItemsSource)
                    {
                        if (s.StatusName == idStatus.Text)
                        {
                            ord.StatusId = s.IdStatus;
                        }
                    }
                    foreach (Delivery d in deliveryGrid.ItemsSource)
                    {
                        if (d.DeliveryCost.ToString() == idDelivery.Text)
                        {
                            ord.DeliveryId = d.IdDelivery;
                        }
                    }
                    await client.PutAsJsonAsync("Orders/" + ((Order)needToDelete).IdOrder, ord);
                    UpdateGrid();
                    break;
                case 9:
                    Position pos = new Position { IdPosition = ((Position)needToDelete).IdPosition, PositionName = posName.Text, PhotoUrl = photo.Text, PositionPrice = double.Parse(price.Text), PositionQuantity = int.Parse(posQ.Text), PositionSlife = posLife.Text, IsExists = (bool)isExists.IsChecked};
                    foreach (Dealer d in dealGrid.ItemsSource)
                    {
                        if (d.DealerName == idDealer.Text)
                        {
                            pos.DealerId = d.IdDealer;
                        }
                    }
                    foreach (Category c in catGrid.ItemsSource)
                    {
                        if (c.CategoryName == idCat.Text)
                        {
                            pos.CategoryId = c.IdCategory;
                        }
                    }
                    await client.PutAsJsonAsync("Positions/" + ((Position)needToDelete).IdPosition, pos);
                    UpdateGrid();
                    break;
                case 10:
                    PositionsInOrder posIn = new PositionsInOrder { IdPositionInOrder = ((PositionsInOrder)needToDelete).IdPositionInOrder};
                    foreach (Position p in posGrid.ItemsSource)
                    {
                        if (p.PositionName == idPosInPosOrd.Text)
                        {
                            posIn.PositionId = p.IdPosition;
                        }
                    }
                    foreach (Order o in orderGrid.ItemsSource)
                    {
                        if (o.OrderNum == idOrdInPosOrd.Text)
                        {
                            posIn.OrderId = o.IdOrder;
                        }
                    }
                    await client.PutAsJsonAsync("PositionsInOrders/" + ((PositionsInOrder)needToDelete).IdPositionInOrder, posIn);
                    UpdateGrid();
                    break;
                case 11:
                    await client.PutAsJsonAsync("Status/" + ((Status)needToDelete).IdStatus, new Status { IdStatus = ((Status)needToDelete).IdStatus, StatusName = statusName.Text});
                    UpdateGrid();
                    break;
                case 12:
                    User user = new User { IdUser = ((User)needToDelete).IdUser, UserLogin = login.Text, UserPassword = password.Text, UserFname = fname.Text, UserLname = lname.Text};
                    foreach (UserRole ur in rolesGrid.ItemsSource)
                    {
                        if (ur.UserRoleName == role.Text)
                        {
                            user.UserRoleId = ur.IdUserRole;
                        }
                    }
                    await client.PutAsJsonAsync("Users/" + ((User)needToDelete).IdUser, user);
                    UpdateGrid();
                    break;
                case 13:
                    await client.PutAsJsonAsync("UserRoles/" + ((UserRole)needToDelete).IdUserRole, new UserRole { IdUserRole = ((UserRole)needToDelete).IdUserRole, UserRoleName = roleName.Text});
                    UpdateGrid();
                    break;
                case 14:
                    UsersDiscount uDisc = new UsersDiscount { IdUserDiscount = ((UsersDiscount)needToDelete).IdUserDiscount };
                    foreach (User u in usersGrid.ItemsSource)
                    {
                        if (u.UserLogin == idUserDisc.Text)
                        {
                            uDisc.UserId = u.IdUser;
                        }
                    }
                    foreach (Discount d in discountGrid.ItemsSource)
                    {
                        if (d.DiscountName == idDisc.Text)
                        {
                            uDisc.DiscountId = d.IdDiscount;
                        }
                    }
                    await client.PutAsJsonAsync("UsersDiscounts/" + ((UsersDiscount)needToDelete).IdUserDiscount, uDisc);
                    UpdateGrid();
                    break;
            }
        }

        private async void delButton_Click(object sender, RoutedEventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    await client.DeleteAsync("Categories/" + ((Category)needToDelete).IdCategory);
                    UpdateGrid();
                    break;
                case 1:
                    await client.DeleteAsync("Dealers/" + ((Dealer)needToDelete).IdDealer);
                    UpdateGrid();
                    break;
                case 2:
                    await client.DeleteAsync("Deliveries/" + ((Delivery)needToDelete).IdDelivery);
                    UpdateGrid();
                    break;
                case 3:
                    await client.DeleteAsync("DeliveryStatus/" + ((DeliveryStatus)needToDelete).IdDeliveryStatus);
                    UpdateGrid();
                    break;
                case 4:
                    await client.DeleteAsync("DeliveryTypes/" + ((DeliveryType)needToDelete).IdDeliveryType);
                    UpdateGrid();
                    break;
                case 5:
                    await client.DeleteAsync("Discounts/" + ((Discount)needToDelete).IdDiscount);
                    UpdateGrid();
                    break;
                case 6:
                    await client.DeleteAsync("DiscountPercents/" + ((DiscountPercent)needToDelete).IdDiscountPercent);
                    UpdateGrid();
                    break;
                case 7:
                    await client.DeleteAsync("Favorites/" + ((Favorite)needToDelete).IdFavorites);
                    UpdateGrid();
                    break;
                case 8:
                    await client.DeleteAsync("Orders/" + ((Order)needToDelete).IdOrder);
                    UpdateGrid();
                    break;
                case 9:
                    await client.DeleteAsync("Positions/" + ((Position)needToDelete).IdPosition);
                    UpdateGrid();
                    break;
                case 10:
                    await client.DeleteAsync("PositionsInOrders/" + ((PositionsInOrder)needToDelete).IdPositionInOrder);
                    UpdateGrid();
                    break;
                case 11:
                    await client.DeleteAsync("Status/" + ((Status)needToDelete).IdStatus);
                    UpdateGrid();
                    break;
                case 12:
                    await client.DeleteAsync("Users/" + ((User)needToDelete).IdUser);
                    UpdateGrid();
                    break;
                case 13:
                    await client.DeleteAsync("UserRoles/" + ((UserRole)needToDelete).IdUserRole);
                    UpdateGrid();
                    break;
                case 14:
                    await client.DeleteAsync("UsersDiscounts/" + ((UsersDiscount)needToDelete).IdUserDiscount);
                    UpdateGrid();
                    break;
            }
        }

        private void dealGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Dealer selectedRow = (Dealer)dealGrid.SelectedItems[0];
                dealName.Text = selectedRow.DealerName;
                dealAddr.Text = selectedRow.DealerAddr;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void statusDelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DeliveryStatus selectedRow = (DeliveryStatus)statusDelGrid.SelectedItems[0];
                statusNameDel.Text = selectedRow.DeliveryStatusName;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void delTypeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DeliveryType selectedRow = (DeliveryType)delTypeGrid.SelectedItems[0];
                delTypeName.Text = selectedRow.DeliveryTypeName;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void deliveryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Delivery selectedRow = (Delivery)deliveryGrid.SelectedItems[0];
                costDel.Text = selectedRow.DeliveryCost.ToString();
                foreach(DeliveryType type in delTypeGrid.ItemsSource)
                {
                    if(type.IdDeliveryType == selectedRow.DeliveryTypeId)
                    {
                        idTypeDel.SelectedItem = type.DeliveryTypeName;
                    }
                }
                foreach (DeliveryStatus status in statusDelGrid.ItemsSource)
                {
                    if (status.IdDeliveryStatus == selectedRow.DeliveryStatusId)
                    {
                        idStatusDel.SelectedItem = status.DeliveryStatusName;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void statusGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Status selectedRow = (Status)statusGrid.SelectedItems[0];
                statusName.Text = selectedRow.StatusName;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void rolesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UserRole selectedRow = (UserRole)rolesGrid.SelectedItems[0];
                roleName.Text = selectedRow.UserRoleName;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void discountPerc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DiscountPercent selectedRow = (DiscountPercent)discountPerc.SelectedItems[0];
                percVal.Text = selectedRow.DiscountPercentValue;
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void discountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Discount selectedRow = (Discount)discountGrid.SelectedItems[0];
                nameDisc.Text = selectedRow.DiscountName;
                foreach(DiscountPercent p in discountPerc.ItemsSource)
                {
                    if(p.IdDiscountPercent == selectedRow.DiscountPercentId)
                    {
                        idDiscPerc.SelectedItem = p.DiscountPercentValue;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void favGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Favorite selectedRow = (Favorite)favGrid.SelectedItems[0];
                foreach (User u in usersGrid.ItemsSource)
                {
                    if (u.IdUser == selectedRow.UserId)
                    {
                        idUser.SelectedItem = u.UserLogin;
                    }
                }
                foreach (Position p in posGrid.ItemsSource)
                {
                    if (p.IdPosition == selectedRow.PositionId)
                    {
                        idPos.SelectedItem = p.PositionName;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                User selectedRow = (User)usersGrid.SelectedItems[0];
                login.Text = selectedRow.UserLogin;
                password.Text = selectedRow.UserPassword;
                fname.Text = selectedRow.UserFname;
                lname.Text = selectedRow.UserLname;
                salt.Text = selectedRow.Salt;
                foreach (UserRole ur in rolesGrid.ItemsSource)
                {
                    if (ur.IdUserRole == selectedRow.UserRoleId)
                    {
                        role.SelectedItem = ur.UserRoleName;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void posGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Position selectedRow = (Position)posGrid.SelectedItems[0];
                posName.Text = selectedRow.PositionName;
                posQ.Text = selectedRow.PositionQuantity.ToString();
                posLife.Text = Convert.ToString(selectedRow.PositionSlife);
                photo.Text = selectedRow.PhotoUrl;
                price.Text = selectedRow.PositionPrice.ToString();
                isExists.IsChecked = selectedRow.IsExists;
                foreach (Dealer d in dealGrid.ItemsSource)
                {
                    if (d.IdDealer == selectedRow.DealerId)
                    {
                        idDealer.SelectedItem = d.DealerName;
                    }
                }
                foreach (Category c in catGrid.ItemsSource)
                {
                    if (c.IdCategory == selectedRow.CategoryId)
                    {
                        idCat.SelectedItem = c.CategoryName;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void posInOrdGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PositionsInOrder selectedRow = (PositionsInOrder)posInOrdGrid.SelectedItems[0];
                foreach (Position p in posGrid.ItemsSource)
                {
                    if (p.IdPosition == selectedRow.PositionId)
                    {
                        idPosInPosOrd.SelectedItem = p.PositionName;
                    }
                }
                foreach (Order o in orderGrid.ItemsSource)
                {
                    if (o.IdOrder == selectedRow.OrderId)
                    {
                        idOrdInPosOrd.SelectedItem = o.OrderNum;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void orderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Order selectedRow = (Order)orderGrid.SelectedItems[0];
                ordNum.Text = selectedRow.OrderNum;
                ordDate.Text = Convert.ToString(selectedRow.OrderDate);
                ordSum.Text = selectedRow.OrderSum.ToString();
                foreach (User u in usersGrid.ItemsSource)
                {
                    if (u.IdUser == selectedRow.UserId)
                    {
                        idUserOrd.SelectedItem = u.UserLogin;
                    }
                }
                foreach (Status s in statusGrid.ItemsSource)
                {
                    if (s.IdStatus == selectedRow.StatusId)
                    {
                        idStatus.SelectedItem = s.StatusName;
                    }
                }
                foreach (Delivery d in deliveryGrid.ItemsSource)
                {
                    if (d.IdDelivery == selectedRow.DeliveryId)
                    {
                        idDelivery.SelectedItem = d.DeliveryCost;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }

        private void discUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UsersDiscount selectedRow = (UsersDiscount)posInOrdGrid.SelectedItems[0];
                foreach (User u in usersGrid.ItemsSource)
                {
                    if (u.IdUser == selectedRow.UserId)
                    {
                        idUserDisc.SelectedItem = u.UserLogin;
                    }
                }
                foreach (Discount d in discountGrid.ItemsSource)
                {
                    if (d.IdDiscount == selectedRow.DiscountId)
                    {
                        idDisc.SelectedItem = d.DiscountName;
                    }
                }
                needToDelete = selectedRow;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
