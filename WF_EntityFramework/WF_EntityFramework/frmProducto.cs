using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_EntityFramework.Model;

namespace WF_EntityFramework
{
    public partial class frmProducto : Form
    {
        private CRUD_ProductoEntities context = new CRUD_ProductoEntities();

        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            updateDGVProductos();
        }

        public void updateDGVProductos()
        {
            //dgvProductos.DataSource = context.Productos.Select(x=> new ProductViewModel { Name = x.Nombre,
            //Descripcion = x.Descripcion,
            //cantidad = x.Stock.Value,
            //ProductId = x.ProductoId}).ToList();

            var products = (from p in context.Productos
                            select new
                            {
                                Nombre = p.Nombre,
                                Descripcion = p.Descripcion
                            }).ToList();

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = context.Productos.ToList();
            dgvProductos.Refresh();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductoDetalle frmProductoDetalle = new frmProductoDetalle();
            frmProductoDetalle.oFrmProducto = this;
            frmProductoDetalle.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvProductos.SelectedRows.Count > 0)
            {
                var product = (Productos)dgvProductos.SelectedRows[0].DataBoundItem;

                DialogResult result = MessageBox.Show("Esta usted seguro de eliminar este producto?", "Alerta!!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var p = context.Productos
                        .Where(x=> x.ProductoId.Equals(product.ProductoId))
                        .SingleOrDefault();
                    
                    context.Productos.Remove(p);

                    context.SaveChanges();
                    
                    MessageBox.Show("Se elimino satisfactoriamente");
                    updateDGVProductos();
                }

            }

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvProductos.SelectedRows.Count > 0)
            {
                var product = (Productos)dgvProductos.SelectedRows[0].DataBoundItem;

                frmProductoDetalle frmProductoDetalle = new frmProductoDetalle();
                frmProductoDetalle.oFrmProducto = this;
                frmProductoDetalle.productId = product.ProductoId;
                frmProductoDetalle.Show();

            }

        }
    }
}
