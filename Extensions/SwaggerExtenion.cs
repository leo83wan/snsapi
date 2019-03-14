using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace com.leo83.apis.sns.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplyTagDescriptions : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<Tag> {
                new Tag { Name = "Common", Description = "通用接口" },
                new Tag { Name = "User", Description = "用户接口" },
                new Tag { Name = "Administrator", Description = "管理员接口" }
            };
        }
    }
}