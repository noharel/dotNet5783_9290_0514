
namespace BlImplementation;

/// <summary>
/// AN ORDER CLASS THAT INMPLEMENTS AN INTERFACE
/// </summary>
internal class Order : BlApi.IOrder
{
    DalApi.IDal Dal = DalApi.Factory.Get(); // DalList object Type

    /// <summary>
    /// GET LIST OF ALL THE ORDERS - FOR MANAGER
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public IEnumerable<BO.OrderForList> GetOrders()
    {
        List<DO.Order?> orderList;
        List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
        try
        {
            orderList = Dal.Order.GetAll().ToList(); // GET ALL THE ORDERS FROM DAL

            orderList.ForEach(delegate (DO.Order? var) // GO THROWGH ALL THE ORDERS
            {
                BO.OrderStatus stat = 0;
                if (var?.ShipDate != null) // ORDER SHIPED
                    stat = (BO.OrderStatus)1;

                if (var?.DeliveryrDate != null) // ORDER DELIVERED
                    stat = (BO.OrderStatus)2;

                int id = (int)(var?.ID)!;
                List<DO.OrderItem?> listOrderItem = Dal.OrderItem.GetListOrder(id).ToList(); // GET ALL ITEMS IN SAME ORDER
                double price = 0;
                listOrderItem.ForEach(delegate (DO.OrderItem? orderItem) {
                    price += (double)(orderItem?.Price * orderItem?.Amount)!; // CALCULATE THE TOTAL PRICE FOR EACH ORDER
                });
                // MAKES THE NEW ORDER
                BO.OrderForList newOrderForList = new BO.OrderForList { ID = id, CustomerName = var?.CustomerName, Status = stat, AmountOfItems = listOrderItem.Count(), TotalPrice = price };
                orderForList.Add(newOrderForList); // ADD THE NEW ORDER TO THE LIST
            });

            //   CHANGE TO LINQ

            //foreach (DO.Order? var in orderList) // GO THROWGH ALL THE ORDERS
            //{
            //    BO.OrderStatus stat = 0;
            //    if (var?.ShipDate != null) // ORDER SHIPED
            //        stat = (BO.OrderStatus)1;

            //    if (var?.DeliveryrDate != null) // ORDER DELIVERED
            //        stat = (BO.OrderStatus)2;

            //    int id = (int)(var?.ID)!; 
            //    IEnumerable<DO.OrderItem?> listOrderItem = Dal.OrderItem.GetListOrder(id); // GET ALL ITEMS IN SAME ORDER
            //    double price = 0;
            //    foreach (DO.OrderItem? orderItem in listOrderItem)
            //    {
            //        price += (double)(orderItem?.Price * orderItem?.Amount)!; // CALCULATE THE TOTAL PRICE FOR EACH ORDER
            //    }
            //    // MAKES THE NEW ORDER
            //    BO.OrderForList newOrderForList = new BO.OrderForList { ID =id, CustomerName = var?.CustomerName, Status = stat, AmountOfItems = listOrderItem.Count(), TotalPrice = price };
            //    orderForList.Add(newOrderForList); // ADD THE NEW ORDER TO THE LIST
            //}

            return orderForList; // RETURNS THE LIST WITH ALL THE ORDERS
        }
        catch (DO.DoesntExistExeption e) // CATCH GETALL FUNCTION EXCEPTION
        {
            throw new BO.DoesntExistExeption("Couldn't get list of orders", e);
        }
    }

    /// <summary>
    /// GET ORDER INFORMATION
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.InvalidInputExeption"></exception>
    public BO.Order OrderInfo(int orderId)
    {
        if ((orderId >= 0)&& (orderId.ToString().Length == 4))  // VALID ORDER ID
        {
            try
            {
                DO.Order newOrder = Dal.Order.GetById(orderId); // GET THE ORDER
                
                try
                {
                    List<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId).ToList(); // GET ALL THE ITEMS IN THE ORDER
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    double totalPriceOrder = 0;

                    listOrder.ForEach(delegate (DO.OrderItem? item) {   // GO THROWGH ALL THE ITEMS
                        // MAKES ITEM
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = Dal.Product.GetById((int)item?.PrudoctID!).Name, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (int)(item?.Amount * item?.Price)! };
                        totalPriceOrder += (double)(item?.Price * item?.Amount)!; // CALCULATE THE TOTAL PRICE

                        BOlistOrder.Add(newOrderItem); // ADD THE ITEM
                    });


                    //          CHANGE TO LINQ

                    //foreach (DO.OrderItem? item in listOrder) // GO THROWGH ALL THE ITEMS
                    //{
                    //    // MAKES ITEM
                    //    BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = newOrder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice =(int)( item?.Amount * item?.Price)! };
                    //    totalPriceOrder += (double)(item?.Price*item?.Amount)!; // CALCULATE THE TOTAL PRICE

                    //    BOlistOrder.Add(newOrderItem); // ADD THE ITEM
                    //}

                    BO.OrderStatus status = BO.OrderStatus.Ordered;  // DEFUALT
                    if (newOrder.ShipDate != null) // IF THE ORDER SHIPED 
                        status = BO.OrderStatus.Shipped;
                    if (newOrder.DeliveryrDate != null) // IF THE ORDER DELIVERED
                        status = BO.OrderStatus.Arrived;

                    // RETURN THE NEW ORDER
                    return new BO.Order() { Items = BOlistOrder, ID = newOrder.ID, CustomerAddress = newOrder.CustomerAddress, CustomerEmail = newOrder.CustomerEmail, ShipDate = newOrder.ShipDate, OrderDate = newOrder.OrderDate, DeliveryrDate = newOrder.DeliveryrDate, CustomerName = newOrder.CustomerName, TotalPrice = totalPriceOrder, Status = status };
                }
                catch (DO.DoesntExistExeption e) // CATCH GETLISTORDER FUNCTION EXCPETION 
                {
                    throw new BO.DoesntExistExeption("Couldn't Get order info",e);
                }

            }
            catch (DO.DoesntExistExeption e) // CATCH GETBYID FUNCTION EXCEPTION
            {
                throw new BO.DoesntExistExeption("couldn't find order", e);
            }
        }
        else //INVALID ORDER ID
        {
            throw new BO.InvalidInputExeption("Invalid order Id");
        }
    }

    /// <summary>
    /// UPDATE SHIP DATE - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="ContradictoryDataExeption"></exception>
    public BO.Order UpdateShip(int orderId)
    {
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderId); // GET THE ORDER
            BO.Order BOorder;

            if ((DOorder.ID >= 0) && DOorder.ID.ToString().Length == 4) // IF ORDER ID IS VALID
            {
                if (DOorder.ShipDate == null) // MANAGER CAN UPDATE SHIP DATE JUST IF THE ORDER IS NOT SHIPED YET
                {
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    List<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId).ToList(); // GET ALL THE ITEMS IN THE ORDER

                    double totalPriceOrder = 0;
                    listOrder.ForEach(delegate (DO.OrderItem? item) {  // GO THROWGH ALL THE ITEMS
                        //CONVERT FROM DO TO BO
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                        BOlistOrder.Add(newOrderItem); // ADD THE ITEM
                        totalPriceOrder += newOrderItem.TotalPrice; // CALCULATE THE TOTAL PRICE OF THE ORDER
                    });

                    //           CHANGE TO LINQ
                    //foreach (DO.OrderItem? item in listOrder) // GO THROWGH ALL THE ITEMS
                    //{
                    //    //CONVERT FROM DO TO BO
                    //    BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                    //    BOlistOrder.Add(newOrderItem); // ADD THE ITEM
                    //    totalPriceOrder += newOrderItem.TotalPrice; // CALCULATE THE TOTAL PRICE OF THE ORDER

                    //}

                    DOorder.ShipDate = DateTime.Now; // UPDATE SHIP DATE
                    // MAKES THE UPDATED ORDER
                    BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = BO.OrderStatus.Shipped };
                    try
                    {
                        Dal.Order.Update(DOorder); // UPDATE THE ORDER
                        return BOorder; // RETURN THE UPDATED ORDER
                    }
                    catch (DO.DoesntExistExeption e) // CATCH UPDATE FUNCTION EXCEPTION
                    {
                        throw new BO.DoesntExistExeption("coudln't update", e);
                    }
                }
                else // ORDER SHIPED ALREADY - CAN'T UPDATE
                {
                    throw new BO.ContradictoryDataExeption("already shipped");
                }
            }
            else // ORDER ID IS NOT VALID
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e)  // CATCH GETBYID FUNCTION EXCEPTION
        { 
            throw new BO.DoesntExistExeption("couldn't get order", e); 
        }
    }

    /// <summary>
    /// UPDATE DELIVERY DATE - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    public BO.Order UpdateDelivery(int orderId)
    {
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderId);// GET THE ORDER
            BO.Order BOorder;

            if ((DOorder.ID >= 0) && (DOorder.ID.ToString().Length == 4)) // IF ORDER ID VALID
            {
                if ((DOorder.ShipDate != null) && (DOorder.DeliveryrDate == null)) // THE ORDER SHIPED BUT NOT DELIVERED YET
                {
                    List<BO.OrderItem> BOlistOrder = new List<BO.OrderItem>();
                    List<DO.OrderItem?> listOrder = Dal.OrderItem.GetListOrder(orderId).ToList(); // GET ALL ITEMS IN THE ORDER

                    double totalPriceOrder = 0;
                    listOrder.ForEach(delegate (DO.OrderItem? item)   // GO THROUGH ALL ITEMS
                    {
                        // CONVERT ITEM FROM 'DO' TO 'BO'
                        BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                        BOlistOrder.Add(newOrderItem); // ADD ITEM
                        totalPriceOrder += newOrderItem.TotalPrice; // CALCULATES THE TOTAL PRICE OF ORDER
                    });


                    ///           CHANGE TO LINQ
                    //foreach (DO.OrderItem? item in listOrder) // GO THROUGH ALL ITEMS
                    //{
                    //    // CONVERT ITEM FROM 'DO' TO 'BO'
                    //    BO.OrderItem newOrderItem = new BO.OrderItem() { ID = (int)item?.ID!, Amount = (int)item?.Amount!, Name = DOorder.CustomerName, Price = (double)item?.Price!, ProductID = (int)item?.PrudoctID!, TotalPrice = (double)(item?.Amount * item?.Price)! };
                    //    BOlistOrder.Add(newOrderItem); // ADD ITEM
                    //    totalPriceOrder += newOrderItem.TotalPrice; // CALCULATES THE TOTAL PRICE OF ORDER

                    //}

                    DOorder.DeliveryrDate = DateTime.Now; // UPDATES DELIVERY DATE

                    // MAKES THE UPDATED ORDER
                    BOorder = new BO.Order() { ID = DOorder.ID, ShipDate = DOorder.ShipDate, CustomerAddress = DOorder.CustomerAddress, CustomerEmail = DOorder.CustomerEmail, CustomerName = DOorder.CustomerName, DeliveryrDate = DOorder.DeliveryrDate, Items = BOlistOrder, TotalPrice = totalPriceOrder, OrderDate = DOorder.OrderDate, Status = BO.OrderStatus.Arrived };
                    try
                    {
                        Dal.Order.Update(DOorder); // UPDATE THE ORDER
                        return BOorder; // RETURN THE UPDETED ORDER
                    }
                    catch (DO.DoesntExistExeption e) // CATCH UPDATE FUNCTION EXCEPTION
                    {
                        throw new BO.DoesntExistExeption("couldn't find", e);
                    }
                }
                else // THE ORDER IS NOT SHIPES YET OR ALREADY DELIVERED
                {
                    throw new BO.ContradictoryDataExeption("order already deliverd or not shipped yet");
                }
            }
            else // INVALID ORDER ID
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e) // CATCH GETBYID FUNCTION EXCEPTION
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }

    /// <summary>
    /// TRACKING ORDER - FOR MANAGER
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public BO.OrderTracking TrackingOrder(int orderId)
    {
        try
        { 
            DO.Order DOorder = Dal.Order.GetById(orderId); // GET ORDER
            if ((DOorder.ID >= 0) && (DOorder.ID.ToString().Length == 4)) // VALID ORDER ID
            {
                List<(DateTime?, string)> tupList = new List<(DateTime?, string)>(); 
                BO.OrderStatus status = BO.OrderStatus.Ordered; //DEFUALT
                tupList.Add((DOorder.OrderDate, "Order was placed"));
                if (DOorder.ShipDate != null) // ORDER SHIPED
                {
                    status = BO.OrderStatus.Shipped;
                    tupList.Add((DOorder.ShipDate, "Order was shipped"));
                }
                if (DOorder.DeliveryrDate != null) // ORDER DELIVERED
                {
                    status = BO.OrderStatus.Arrived;
                    tupList.Add((DOorder.DeliveryrDate, "Order was arrived"));
                }
                // MAKES THE ORDER TRACKING ENTITY
                BO.OrderTracking orderTracking = new BO.OrderTracking() { ID = orderId, Status = status, tuplesList = tupList! };
                return orderTracking; // RETURN THE ENTITY
            }
            else // INVALID ORDER ID
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e) // CATCH GETBYID FUNCTION EXCEPTION
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }

    /// <summary>
    /// BONUS FUNCTION - UPDATE AMOUNT BY MANAGER - FOR MANAGER
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="orderItemId"></param>
    /// <param name="amount"></param>
    /// <exception cref="BO.ContradictoryDataExeption"></exception>
    /// <exception cref="BO.DoesntExistExeption"></exception>
    public void UpdateByManager(int orderID, int orderItemId, int amount )
    {
        try
        {
            DO.Order DOorder = Dal.Order.GetById(orderID); // GET THE ORDER
            if ((DOorder.ID >= 0) && (DOorder.ID).ToString().Length == 4) // VALID ORDER ID
            {
                if (DOorder.ShipDate == null) // ORDER NOT SHIPED - MANAGER CAN UPDTE
                {
                    try
                    {
                        DO.OrderItem DOorderItem = (DO.OrderItem)Dal.OrderItem.GetProduct(orderID, orderItemId)!;
                        if ((DOorderItem.Amount += amount) == 0)//UPDATE AMOUNT
                        {
                            try
                            {
                                Dal.OrderItem.Delete(DOorderItem.ID);
                            }
                            catch(DO.DoesntExistExeption e)
                            {
                                throw new BO.DoesntExistExeption("couldn't delete product", e);
                            }
                        }
                        else    Dal.OrderItem.Update(DOorderItem);
                    }
                    catch(DO.DoesntExistExeption e)
                    {
                        throw new BO.DoesntExistExeption("couldn't get product", e);
                    }
                }

                
                else // ORDER SHIPED - MANGER CAN'T UPDATE
                {
                    throw new BO.ContradictoryDataExeption("Order was already shipped, can not update it");
                }
            }
            else //INVALID ORDER ID
            {
                throw new BO.DoesntExistExeption("order does not exsist");
            }
        }
        catch (DO.DoesntExistExeption e) // CATCH GETBYID FUNCTION EXCEPTION
        {
            throw new BO.DoesntExistExeption("couldn't get order", e);
        }

    }
}
