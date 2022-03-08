using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Domain.Base
{
    public abstract class BaseEntity
    {

        /// <summary>
        /// Identificador do produto.
        /// </summary>

        [BsonId]
        public string Id { get; protected set; }



        /// <summary>
        /// <value>True se ativo.</value>
        /// </summary>
        public bool Active { get; protected set; }


        /// <summary>
        /// <value>True se Excluido.</value>
        /// </summary>
        public bool Deleted { get; protected set; }


        /// <summary>
        /// <value>Data da efetivação da exclusão.</value>
        /// </summary>
        public Nullable<DateTime> DeleteAt { get; set; }

        /// <summary>
        /// <value>Data da efetivação do cadastro do Produto.</value>
        /// </summary>
        public Nullable<DateTime> CreatedAt { get; set; }

        /// <summary>
        /// Data de ultima modificação executada
        /// </summary>
        public Nullable<DateTime> ModifiedAt { get; set; }



        public abstract void Delete();
        public abstract void Actived();
        public abstract void Deactived();


        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;
            return Id.Equals(compareTo.Id);
        }


        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }



        public static bool operator !=(BaseEntity a, BaseEntity b)
        {
            return !(a == b);
        }


        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 777) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id = " + Id + "]";
        }

    }
}
