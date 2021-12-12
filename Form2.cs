using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //This allows for the use of MS Access DB Files

namespace Milestone2_Invenetory
{
    public partial class Form2 : Form
    {//Below is for DB access and manipulation
        private OleDbConnection mycon;

        private OleDbDataReader dr = null;

        private OleDbCommand cmd = null;

        string functioncall, codedisplay, namedisplay, srpdisplay, costdisplay, stockdisplay;

        string product;

        public Form2(string function, string code, string name, string srp, string cost, string stock)
        {//DB variable is established with the mycon at page load Thhe others are eplained with their name for the DB 

            codedisplay = code;

            namedisplay = name;

            srpdisplay = srp;

            costdisplay = cost;

            stockdisplay = stock;

            functioncall = function;

            InitializeComponent();

            mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=inventory.accdb");
        }

        private void Form2_Load(object sender, EventArgs e)
        {// on load this is delcaired
            product = codeTextBox1.Text;

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (functioncall == "add")

            {
                //Calls to the add function 
                add();

                this.Hide();

            }

            if (functioncall == "update")

            {
                // Calls to the update function
                update();

                this.Hide();

            }
        }
        //the add fuction
        private void add()

        {
            // Below first checks for product in the db file before adding
            mycon.Open();
            //Below is for if Meat Radio is checked it will display the meatDB
            if (meatRadioButton.Checked)
            {
                //Below uses a string to check by product ID which is textbox1
                string cmdstr = "SELECT * FROM meatproducts WHERE product_id ='" + codeTextBox1.Text + "'";
                
                cmd = new OleDbCommand(cmdstr, mycon);

                dr = cmd.ExecuteReader();
            }
            //Below is for if fruit Radio is checked it will display the fruitDB

            if (fruitRadioButton.Checked)
            {

                string cmdstr = "SELECT * FROM products WHERE product_id ='" + codeTextBox1.Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                dr = cmd.ExecuteReader();
            }
            //Below is for if veggies Radio is checked it will display the veggiesDB

            if (veggiesRadioButton.Checked)
            {

                string cmdstr = "SELECT * FROM veggiesproducts WHERE product_id ='" + codeTextBox1.Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                dr = cmd.ExecuteReader();
            }
            //same as above, so below
            if (dairyRadioButton.Checked)
            {

                string cmdstr = "SELECT * FROM dairyproducts WHERE product_id ='" + codeTextBox1.Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                dr = cmd.ExecuteReader();
            }
            //same as above, so below

            if (dryRadioButton.Checked)
            {

                string cmdstr = "SELECT * FROM dryproducts WHERE product_id ='" + codeTextBox1.Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                dr = cmd.ExecuteReader();
            }

            if (dr.HasRows == true)
                //Below will show a popup saying if it already is in invenetory

            {

                MessageBox.Show("ENTRY EXISTS!", "ERROR");

                dr.Close();

                mycon.Close();

            }

            else if (dr.HasRows == false)
                //Below will add the meat product if it is not in inventory
            {
                if (meatRadioButton.Checked)
                { 
                    //string that is in the format to insert meat items data into the meat db
                    string cmdstr2 = "INSERT INTO meatproducts([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                    cmd = new OleDbCommand(cmdstr2, mycon);

                    cmd.ExecuteNonQuery();

                    dr.Close();
                    //closes connection
                    mycon.Close();
                    //shows a message if it was added
                    MessageBox.Show("MEAT PRODUCT ADDED!", "CONFIRMATION");
                    
                }
               

                if (fruitRadioButton.Checked)
                {//below will insert into proper column 
                    string cmdstr2 = "INSERT INTO products([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";
                    
                    cmd = new OleDbCommand(cmdstr2, mycon);

                    cmd.ExecuteNonQuery();

                    dr.Close();

                    mycon.Close();

                    MessageBox.Show("FRUIT PRODUCT ADDED!", "CONFIRMATION");
                }
                

                if (veggiesRadioButton.Checked)
                {//below will insert into proper column 
                    string cmdstr2 = "INSERT INTO veggiesproducts([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                    cmd = new OleDbCommand(cmdstr2, mycon);

                    cmd.ExecuteNonQuery();

                    dr.Close();

                    mycon.Close();

                    MessageBox.Show("VEGGIE PRODUCT ADDED!", "CONFIRMATION");
                }
                

                if (dairyRadioButton.Checked)
                {//below will insert into proper column 
                    string cmdstr2 = "INSERT INTO dairyproducts([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                    cmd = new OleDbCommand(cmdstr2, mycon);

                    cmd.ExecuteNonQuery();

                    dr.Close();

                    mycon.Close();

                    MessageBox.Show("DAIRY PRODUCT ADDED!", "CONFIRMATION");
                }
                

                if (dryRadioButton.Checked)
                {//below will insert into proper column 
                    string cmdstr2 = "INSERT INTO dryproducts([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                    cmd = new OleDbCommand(cmdstr2, mycon);

                    cmd.ExecuteNonQuery();

                    dr.Close();

                    mycon.Close();

                    MessageBox.Show("DRY PRODUCT ADDED!", "CONFIRMATION");
                }
                
             }

        }
        //Update function
        private void update()
        //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/generating-commands-with-commandbuilders

        {
            //below will insert the text into the proper db file and column
            mycon.Open();


            if (meatRadioButton.Checked)
            {
                //string that is in the format to insert meat items data into the meat db

                string cmdstr = "UPDATE meatproducts SET product_id = '" + codeTextBox1.Text + "', product_name='" + nameTextBox2.Text + "', product_srp='" + Convert.ToDouble(srpTextBox3.Text) + "',product_cost='" + Convert.ToDouble(costTextBox4.Text) + "', product_stock='" + Convert.ToDouble(stockTextBox5.Text) + "' WHERE product_id = '" + product + "'";

                //string cmdstr = "UPDATE meatproducts ([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                //string cmdstr = "INSERT INTO meatproducts([product_id],[product_name],[product_srp],[product_cost],[product_stock]) VALUES('" + codeTextBox1.Text + "','" + nameTextBox2.Text + "','" + Convert.ToDouble(srpTextBox3.Text) + "','" + Convert.ToDouble(costTextBox4.Text) + "','" + Convert.ToInt32(stockTextBox5.Text) + "')";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();

                MessageBox.Show("MEAT PRODUCT Updated!", "CONFIRMATION");
             
                   

                // cmd = new OleDbCommand(cmdstr, mycon);

                // cmd.ExecuteNonQuery();

                // mycon.Close();

                //shows a message if it was added
            }
            //below will insert the text into the proper db file and column

            if (fruitRadioButton.Checked)
            {

                string cmdstr = "UPDATE products SET product_id = '" + codeTextBox1.Text + "', product_name='" + nameTextBox2.Text + "', product_srp='" + Convert.ToDouble(srpTextBox3.Text) + "',product_cost='" + Convert.ToDouble(costTextBox4.Text) + "', product_stock='" + Convert.ToDouble(stockTextBox5.Text) + "' WHERE product_id = '" + product + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //below will insert the text into the proper db file and column

            if (veggiesRadioButton.Checked)
            {

                string cmdstr = "UPDATE veggiesproducts SET product_id = '" + codeTextBox1.Text + "', product_name='" + nameTextBox2.Text + "', product_srp='" + Convert.ToDouble(srpTextBox3.Text) + "',product_cost='" + Convert.ToDouble(costTextBox4.Text) + "', product_stock='" + Convert.ToDouble(stockTextBox5.Text) + "' WHERE product_id = '" + product + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //below will insert the text into the proper db file and column

            if (dairyRadioButton.Checked)
            {

                string cmdstr = "UPDATE dairyproducts SET product_id = '" + codeTextBox1.Text + "', product_name='" + nameTextBox2.Text + "', product_srp='" + Convert.ToDouble(srpTextBox3.Text) + "',product_cost='" + Convert.ToDouble(costTextBox4.Text) + "', product_stock='" + Convert.ToDouble(stockTextBox5.Text) + "' WHERE product_id = '" + product + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //below will insert the text into the proper db file and column

            if (dryRadioButton.Checked)
            {

                string cmdstr = "UPDATE dryproducts SET product_id = '" + codeTextBox1.Text + "', product_name='" + nameTextBox2.Text + "', product_srp='" + Convert.ToDouble(srpTextBox3.Text) + "',product_cost='" + Convert.ToDouble(costTextBox4.Text) + "', product_stock='" + Convert.ToDouble(stockTextBox5.Text) + "' WHERE product_id = '" + product + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }

        }
        //The exit button for the form two update/ add portion
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
        //The product ID Box for user input
        private void codeTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //the product name user input box
        private void nameTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //the sugested retail price user input box
        private void srpTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        //the company cost user input box
        private void costTextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        //the stock level user input box
        private void stockTextBox5_TextChanged(object sender, EventArgs e)
        {

        }
        //This picture box just acts as a container for radio buttons
        private void radioPictureBox_Click(object sender, EventArgs e)
        {

        }
        //meat db radio button
        private void meatRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        //fruit db radio button
        private void fruitRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        //veggies radio button
        private void veggiesRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        //dairy radio button
        private void dairyRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        //dry foods radio button
        private void dryRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
        //Product ID Label
        private void codeLabel_Click(object sender, EventArgs e)
        {

        }
        //Product Name Label
        private void nameLabel_Click(object sender, EventArgs e)
        {

        }
        //SRP Label
        private void srpLabel_Click(object sender, EventArgs e)
        {

        }
        //Cost Label
        private void costLabel_Click(object sender, EventArgs e)
        {

        }
        //stock label
        private void stockLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
