using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Data.Repositories;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Models.Entities;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.Helpers;
using CloudCityCakesMVC.Models.ViewModels;
using CloudCityCakesMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CloudCityCakesMVC.Services.Rules;

namespace CloudCityCakesMVC.Services.Implementations
{
    
    //https://codepen.io/Layla-P/pen/zYGegdq?editors=1111
    public class CakeOrderService : ICakeOrderService
    {
        private readonly ICakeOrderRepository _cakeOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationHandler _notificationHandler;

        public CakeOrderService(ICakeOrderRepository cakeOrderRepository, IUserRepository userRepository, INotificationHandler notificationHandler)
        {
            _cakeOrderRepository = cakeOrderRepository ?? throw new ArgumentNullException(nameof(cakeOrderRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
        }


        public async Task<ServiceResponse<CakeOrderViewModel>> GetCakeOrderByIdAsync(int id)
        {
            var serviceResponse = new ServiceResponse<CakeOrderViewModel>();

            var cakeOrder = await _cakeOrderRepository
                .GetById(id)
                .ConfigureAwait(false);
           
            if (cakeOrder == null)
            {
                serviceResponse.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                return serviceResponse;
            }
            var user = await _userRepository.GetById(cakeOrder.UserId);
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;
            serviceResponse.Content = new CakeOrderViewModel(cakeOrder, user);

            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersAsync()
        {
            var serviceResponse = new ServiceResponse<IList<CakeOrderViewModel>>();

            var orders = await _cakeOrderRepository
                .GetAll()
                .ConfigureAwait(false);

            if (orders == null)
            {
                serviceResponse.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                return serviceResponse;
            }

            var cakeOrderEntities = orders.Include("User").ToList();
            var cakeOrders = new List<CakeOrderViewModel>();
            foreach (var order in cakeOrderEntities)
            {
                var vm = new CakeOrderViewModel(order, order.User);
                cakeOrders.Add(vm);
            }
            
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;
            serviceResponse.Content = cakeOrders;

            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersAsync(OrderStatus status)
        {
            var serviceResponse = new ServiceResponse<IList<CakeOrderViewModel>>();

            var orders = await _cakeOrderRepository
                .GetAll()
                .ConfigureAwait(false);

            if (orders == null)
            {
                serviceResponse.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                return serviceResponse;
            }

            var cakeOrderEntities = orders.Where(e => e.OrderStatus == status).Include("User").ToList();
            var cakeOrders = new List<CakeOrderViewModel>();
            foreach (var order in cakeOrderEntities)
            {
                var vm = new CakeOrderViewModel(order, order.User);
                cakeOrders.Add(vm);
            }
            
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;
            serviceResponse.Content = cakeOrders;

            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersByUserIdAsync(int userId)
        {
            var serviceResponse = new ServiceResponse<IList<CakeOrderViewModel>>();

            var orders = await _cakeOrderRepository
                .GetAll()
                .ConfigureAwait(false);

            if (orders == null)
            {
                serviceResponse.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                return serviceResponse;
            }

            var cakeOrderEntities = orders.Where(e => e.UserId == userId).ToList();
            var cakeOrders = new List<CakeOrderViewModel>();
            foreach (var order in cakeOrderEntities)
            {
                var vm = new CakeOrderViewModel(order);
                cakeOrders.Add(vm);
            }

            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;
            serviceResponse.Content = cakeOrders;

            return serviceResponse;
        }


        public async Task<ServiceResponse<CakeOrderViewModel>> AddNewOrderAsync(OrderDetails orderDetails)
        {
            var serviceResponse = new ServiceResponse<CakeOrderViewModel>();

            var cakeOrder = new CakeOrder
            {
                Frosting = orderDetails.Cake.Frosting,
                Flavour = orderDetails.Cake.Flavour,
                Topping = orderDetails.Cake.Topping,
                Size = orderDetails.Cake.Size,
                Price = orderDetails.Cake.Price,
                OrderDate = DateTime.UtcNow,
                OrderStatus = OrderStatus.New
            };
            User userEntity;
            if (!orderDetails.IsNewUser)
            {
                userEntity = await _userRepository
                    .GetByPhoneNumber(orderDetails.Number)
                    .ConfigureAwait(false);
            }
            else
            {
                var user = new User
                {
                    Email = orderDetails.Email,
                    PhoneNumber = orderDetails.Number,
                    Name = orderDetails.Name
                };
                userEntity = await _userRepository
                    .Add(user)
                    .ConfigureAwait(false);
            }

            cakeOrder.UserId = userEntity.Id;

            var cakeOrderEntity = await _cakeOrderRepository
                .Add(cakeOrder)
                .ConfigureAwait(false);

            serviceResponse.Content = new CakeOrderViewModel(cakeOrderEntity, userEntity);;
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Created;

            return serviceResponse;
        }

        public async Task<ServiceResponse<CakeOrderViewModel>> UpdateOrderStatusAsync(CakeOrderViewModel vm)
        {
            var serviceResponse = new ServiceResponse<CakeOrderViewModel>();
            var entity = await _cakeOrderRepository.GetById(vm.Id);
            entity.OrderStatus = vm.OrderStatus;
            var cakeOrderEntity = await _cakeOrderRepository.Update(entity);
            
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;
            serviceResponse.Content = vm;

            await _notificationHandler.SendNotification(cakeOrderEntity.Id);
            
            return serviceResponse;
        }
    }
}