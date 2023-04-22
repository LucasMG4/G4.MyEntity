using G4.MyEntity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4.MyEntity.Controllers {
    internal static class MappingController {

        public static List<EntityParameter> TransformObjectToParameters(this object entity) {

            var result = new List<EntityParameter>();

            entity.GetType().GetProperties().ToList().ForEach(property => {
                result.Add(new EntityParameter(property.Name, property.GetValue(entity)));
            });

            return result;

        }

    }
}
