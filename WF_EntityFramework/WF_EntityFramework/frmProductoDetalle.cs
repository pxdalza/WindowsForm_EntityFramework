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
    public partial class frmProductoDetalle : Form
    {
        public int productId = 0;
        private CRUD_ProductoEntities context = new CRUD_ProductoEntities();
        public frmProducto oFrmProducto = new frmProducto();

        public frmProductoDetalle()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (productId == 0)
                {
                    context.Productos.Add(new Productos { 
                    Nombre  = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text)
                    });
                    context.SaveChanges();
                    MessageBox.Show("Se registro satisfactoriamente.");
                }
                else
                {

                    var product = context.Productos
                        .Where(x => x.ProductoId.Equals(productId))
                        .SingleOrDefault();
                    //SingleOrDefault -> le indicas q el objeto puede q exista o no
                    //Single  -> le indicas q el objeto existe en bd.

                    if (product != null)
                    {
                        product.Nombre = txtNombre.Text;
                        product.Descripcion = txtDescripcion.Text;
                        product.Precio = decimal.Parse(txtPrecio.Text);
                        product.Stock = int.Parse(txtStock.Text);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Se actualizo satisfactoriamente.");

                }

                
                oFrmProducto.updateDGVProductos();

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmProductoDetalle_Load(object sender, EventArgs e)
        {

            if (productId != 0)
            {
                var product = context.Productos
                      .Where(x => x.ProductoId.Equals(productId))
                      .SingleOrDefault();

                txtNombre.Text = product.Nombre;
                txtDescripcion.Text = product.Descripcion;
                txtPrecio.Text = product.Precio.ToString();
                txtStock.Text = product.Stock.ToString();
            }
           
        }
    }
}
