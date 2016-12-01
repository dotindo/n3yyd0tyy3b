using System;
using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;
using DotWeb.Utils;
using NLog;

namespace DotWeb.Repositories
{
    public class ModelRepository
    {
        public static Model RetrieveModelById(int id)
        {
            try
            {
                using (AppDb context = new AppDb())
                {
                    return context.Models.FirstOrDefault(p => p.Id == id);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex);
            }
            return null;
        }

        public static Variant RetrieveModelVariantById(int variantId)
        {
            using (AppDb context = new AppDb())
            {
                return context.Variants.FirstOrDefault(p => p.Id == variantId);
            }
        }

        public static List<Variant> RetrieveVariantByModelId(int modelId)
        {
            using (AppDb context = new AppDb())
            {
                return context.Variants.Where(p => p.ModelId == modelId).ToList();
            }
        }

        public static List<Model> RetrieveModels()
        {
            using (AppDb context = new AppDb())
            {
                return context.Models.ToList();
            }
        }

        public static List<Variant> RetrieveVariants()
        {
            using (AppDb context = new AppDb())
            {
                return context.Variants.ToList();
            }
        }

        public static int RetrieveModelIdByName(string modelName)
        {
            int modelId = 0;
            using (AppDb context = new AppDb())
            {
                Model model = context.Models.FirstOrDefault(p => p.ModelName == modelName);
                if (model != null)
                    modelId = model.Id;
            }
            return modelId;
        }

        public static int RetrieveVariantIdByModelId(int modelId)
        {
            using (AppDb context = new AppDb())
            {
                Variant variant = context.Variants.FirstOrDefault(p => p.ModelId == modelId);
                if (variant != null)
                    return variant.Id;
            }
            return 0;
        }

        public static int RetrieveVariantIdByModelIdAndVariantName(int modelId, string variantName)
        {
            using (AppDb context = new AppDb())
            {
                Variant variant = context.Variants.FirstOrDefault(p => p.ModelId == modelId && p.Variant1 == variantName);
                if (variant != null) return variant.Id;
            }
            return 0;
        }

        public static string RetrieveVariantNameByModelIdAndVariantId(int modelId, int variantId)
        {
            using (AppDb context = new AppDb())
            {
                Variant variant = context.Variants.FirstOrDefault(p => p.ModelId == modelId && p.Id == variantId);
                if (variant != null) return variant.Variant1;
            }
            return "";
        }

        public static string RetrieveTypeNameById(int id)
        {
            using (AppDb context = new AppDb())
            {
                Models.Type type = context.Types.FirstOrDefault(p => p.Id == id);
                if (type != null)
                    return type.Type1;
            }
            return "";
        }
    }
}
