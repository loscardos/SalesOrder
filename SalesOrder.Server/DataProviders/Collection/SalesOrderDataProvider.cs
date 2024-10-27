using Microsoft.EntityFrameworkCore;
using SalesOrder.Server.BindingModels;
using SalesOrder.Server.Infrastructure;
using SalesOrder.Shared;
using static SalesOrder.Server.BindingModels.BaseResponseModel;

namespace SalesOrder.Server.DataProviders.Collection;

public class SalesOrderDataProvider : ISalesOrderDataProvider
{
    private readonly OlDevContext _context;

    private readonly DbSet<SoOrder> _soOrders;
    private readonly DbSet<ComCustomer> _comCustomers;

    public SalesOrderDataProvider(
        OlDevContext context
    )
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _soOrders = _context.SoOrders;
        _comCustomers = _context.ComCustomers;
    }

    public async Task<Responses<SoOrder>> Get(SalesOrderListModel salesOrderListModel)
    {
        var pageSize = salesOrderListModel.PageSize ?? 0;
        var currentPage = salesOrderListModel.CurrentPage ?? 1;

        List<SoOrder> soOrder;

        IQueryable<SoOrder> query = _soOrders.AsQueryable()
            .Include(x => x.ComCustomer)
            .Include(x => x.SoItems)
            .Where(x =>
                (string.IsNullOrEmpty(salesOrderListModel.Search) ||
                 EF.Functions.Like(x.OrderNo.ToLower(), "%" + salesOrderListModel.Search.ToLower() + "%")) &&
                (!salesOrderListModel.OrderDate.HasValue ||
                 x.OrderDate.Date == salesOrderListModel.OrderDate.Value.Date)
            );

        int totalItem = await query.CountAsync();

        if (pageSize > 0)
        {
            soOrder = await query.Skip(Math.Min(pageSize * (currentPage - 1), totalItem)).Take(pageSize).ToListAsync();
            int totalPages = (int)Math.Ceiling((double)totalItem / pageSize);

            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = soOrder,
                Meta = new()
                {
                    TotalRecords = totalItem,
                    TotalPages = totalPages,
                }
            };
        }

        soOrder = await query.ToListAsync();

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = soOrder,
            Meta = new()
            {
                TotalRecords = totalItem,
                TotalPages = 1
            }
        };
    }

    public async Task<Responses<ComCustomer>> GetCustomers()
    {
        List<ComCustomer> customers = await _comCustomers.AsQueryable()
            .ToListAsync();

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = customers
        };
    }

    public async Task<SoOrder?> GetById(int id)
    {
        SoOrder? soOrder = await _soOrders
            .AsNoTracking()
            .Include(x => x.ComCustomer)
            .Include(x => x.SoItems)
            .Where(x => x.SoOrderId.Equals(id))
            .FirstOrDefaultAsync();

        if (soOrder == null)
            return null;

        return soOrder;
    }

    public async Task<SoOrder?> Create(SoOrder soOrder)
    {
        await _soOrders.AddAsync(soOrder);
        await _context.SaveChangesAsync();

        return soOrder;
    }

    public async Task<SoOrder?> Update(SoOrder soOrder)
    {
        SoOrder? existingSoOrder = await _soOrders
            .Include(x => x.SoItems)
            .Include(o => o.ComCustomer)
            .Where(x => x.SoOrderId.Equals(soOrder.SoOrderId))
            .FirstOrDefaultAsync();

        if (existingSoOrder == null) return null;

        _context.Entry(existingSoOrder).CurrentValues.SetValues(soOrder);
        
        if (soOrder.SoItems.Any())
        {
            var existingItems = existingSoOrder.SoItems.ToList();
            foreach (var existingItem in existingItems)
            {
                if (soOrder.SoItems.All(i => i.SoItemId != existingItem.SoItemId))
                    _context.Remove(existingItem);
            }
        
            foreach (var item in soOrder.SoItems)
            {
                var existingItem = existingItems.FirstOrDefault(i => i.SoItemId == item.SoItemId);
                if (existingItem != null)
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                else
                    existingSoOrder.SoItems.Add(item);
            }
        }

        await _context.SaveChangesAsync();

        return soOrder;
    }

    public async Task<SoOrder?> Delete(int soOrderId)
    {
        SoOrder? soOrderToDelete = await GetById(soOrderId);

        if (soOrderToDelete == null)
            return null;

        _soOrders.Remove(soOrderToDelete);
        await _context.SaveChangesAsync();

        return soOrderToDelete;
    }
}