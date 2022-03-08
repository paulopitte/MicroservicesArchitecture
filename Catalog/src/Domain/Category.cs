using Catalog.Api.Domain.Base;

namespace Catalog.Api.Domain
{
    public class Category : BaseEntity
    {

        public Category(string name)
        {
            Name = name;
        }
        public string Name { get;}



        /// <summary>
        /// Ativar
        /// Marque a caixa para publicar este produto (visível na loja e estoque).
        /// Desmarque para false para não publicar (produto não disponível na loja e estoque).
        /// </summary>
        public override void Actived() => this.Active = true;

        /// <summary>
        /// Desativar
        /// Marque a caixa para publicar este produto (visível na loja e estoque).
        /// Desmarque para false para não publicar (produto não disponível na loja e estoque).
        /// </summary>
        public override void Deactived() => this.Active = false;

        public override void Delete()
        {
            this.Deleted = true;
            this.Active = false;
            this.DeleteAt = DateTime.UtcNow;
        }
    }
}
