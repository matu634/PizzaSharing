using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepositoryAsync : BaseRepositoryAsync<Price>, IPriceRepository
    {
        public PriceRepositoryAsync(IDataContext dataContext) : base(dataContext)
        {
        }

        public override async Task<IEnumerable<Price>> AllAsync()
        {
            return await RepoDbSet
                .Include(price => price.Product)
                .Include(price => price.Change)
                .ToListAsync();
        }

        public override async Task<Price> FindAsync(params object[] id)
        {
            var price = await RepoDbSet.FindAsync(id);

            if (price != null)
            {
                await RepoDbContext.Entry(price).Reference(p => p.Change).LoadAsync();
                await RepoDbContext.Entry(price).Reference(p => p.Product).LoadAsync();
            }

            return price;
        }

        public async Task AddAsync(DALPriceDTO priceDTO)
        {
            if ((priceDTO.ChangeId != null && priceDTO.ProductId != null) ||
                (priceDTO.ChangeId == null && priceDTO.ProductId == null))
                throw new ArgumentException("Both priceId and product id can't be null or both can't have a value");
            var price = new Price()
            {
                Value = priceDTO.Value,
                ChangeId = priceDTO.ChangeId,
                ProductId = priceDTO.ProductId,
                ValidFrom = priceDTO.ValidFrom,
                ValidTo = priceDTO.ValidTo
            };

            await RepoDbSet.AddAsync(price);
        }

        public async Task EditAsync(DALPriceDTO priceDTO)
        {
            if ((priceDTO.ChangeId != null && priceDTO.ProductId != null) ||
                (priceDTO.ChangeId == null && priceDTO.ProductId == null))
                throw new ArgumentException("Both priceId and product id can't be null or both can't have a value");
            
            var time = priceDTO.ValidFrom;
            
            if (priceDTO.ProductId != null)
            {
                var oldPrice = await RepoDbSet
                    .Where(price => price.ValidTo > time && price.ValidFrom < time &&
                                    price.ProductId == priceDTO.ProductId)
                    .SingleOrDefaultAsync();
                oldPrice.ValidTo = time;
            }
            else
            {
                var oldPrice = await RepoDbSet
                    .Where(price => price.ValidTo > time && price.ValidFrom < time &&
                                    price.ChangeId == priceDTO.ChangeId)
                    .SingleOrDefaultAsync();
                oldPrice.ValidTo = time;
            }
            
            
            
            var newPrice = new Price()
            {
                Value = priceDTO.Value,
                ChangeId = priceDTO.ChangeId,
                ProductId = priceDTO.ProductId,
                ValidTo = priceDTO.ValidTo,
                ValidFrom = priceDTO.ValidFrom
            };

            await RepoDbSet.AddAsync(newPrice);
        }
    }
}