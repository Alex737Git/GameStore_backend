using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        // var strategy = new Category()
        // {
        //     Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //     Title = "Strategy",
        // };
        //
        // strategy.Children = new List<Category>()
        // {
        //     new Category
        //     {
        //         Id = new Guid("c8d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Title = "Rally",
        //         ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Parent = strategy
        //     },
        //     new Category
        //     {
        //         Id = new Guid("c7d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Title = "Arcade",
        //         ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Parent = strategy
        //     },
        //     new Category
        //     {
        //         Id = new Guid("c6d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Title = "Formula",
        //         ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Parent = strategy
        //     },
        //     new Category
        //     {
        //         Id = new Guid("c5d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Title = "Off-road",
        //         ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //         Parent = strategy
        //     },
        // };

        // builder.HasData();

        // builder.HasData(
        // new Category
        //       {
        //           Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
        //           Title = "Strategy",
        //           Children = new List<Category>
        //           {
        //               new Category
        //               {
        //                   Id = new Guid("c8d4c053-49b6-410c-bc78-2d54a9991870"),
        //                   Title = "Rally",
        //                   ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
        //               },
        //               new Category
        //               {
        //                   Id = new Guid("c7d4c053-49b6-410c-bc78-2d54a9991870"),
        //                   Title = "Arcade",
        //                   ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
        //               },
        //               new Category
        //               {
        //                   Id = new Guid("c6d4c053-49b6-410c-bc78-2d54a9991870"),
        //                   Title = "Formula",
        //                   ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
        //               },
        //               new Category
        //               {
        //                   Id = new Guid("c5d4c053-49b6-410c-bc78-2d54a9991870"),
        //                   Title = "Off-road",
        //                   ParentId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
        //               },
        //           }
        //       },
        //       new Category
        //       {
        //           Id = new Guid("c9d5c053-49b6-410c-bc78-2d54a9991870"),
        //           Title = "Rpg",
        //       },
        //       new Category
        //       {
        //           Id = new Guid("c9d6c053-49b6-410c-bc78-2d54a9991870"),
        //           Title = "Sports",
        //       },
        //       new Category
        //       {
        //           Id = new Guid("c9d7c053-49b6-410c-bc78-2d54a9991870"),
        //           Title = "Races",
        //       },
        //       new Category
        //       {
        //           Id = new Guid("c9d8c053-49b6-410c-bc78-2d54a9991870"),
        //           Title = "Action",
        //           Children = new List<Category>
        //           {
        //               new Category
        //               {
        //                   Id = new Guid("b789608e-9a1b-40ab-952a-c73611042e2d"),
        //                   Title = "Fps",
        //                   ParentId = new Guid("c9d8c053-49b6-410c-bc78-2d54a9991870"),
        //               },
        //               new Category
        //               {
        //                   Id = new Guid("db38905f-a886-4ac1-8573-40b15cf8d2e9"),
        //                   Title = "Tps",
        //                   ParentId = new Guid("c9d8c053-49b6-410c-bc78-2d54a9991870"),
        //               },
        //               new Category
        //               {
        //                   Id = new Guid("1fe43a5c-9f0f-4089-b800-47b1a3e1206d"),
        //                   Title = "Misc",
        //                   ParentId = new Guid("c9d8c053-49b6-410c-bc78-2d54a9991870"),
        //               }
        //           }
        //       },
        //       new Category
        //       {
        //           Id = new Guid("0508c103-d0a9-472a-867c-2240ec822c6f"),
        //           Title = "Adventure",
        //       },
        //       new Category
        //       {
        //           Id = new Guid("d65dd8d2-f354-4a6b-9235-a7a2034f2dc8"),
        //           Title = "Puzzle & skill",
        //       },
        //       new Category
        //       {
        //           Id = new Guid("fa123a5e-1625-481f-8781-3a9e454d4bc5"),
        //           Title = "Other",
        //       }
        // );
    }
    
}