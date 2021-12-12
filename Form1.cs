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
    public partial class Form1 : Form
    {//Below is for DB access and manipulation
        private OleDbConnection mycon;

        private OleDbDataReader dr = null;

        private OleDbCommand cmd = null;

        public Form1()
        {
            //DB variable is established with the mycon at page load
            InitializeComponent();
            mycon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=inventory.accdb");
        }
        //Below is the list view window that will display all the information from the DB file
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Below is the add Item to DB button
        private void addButton_Click(object sender, EventArgs e)
        {//Below is the string format for the items in the colomns of the DB file
            Form infobox = new Form2("add", "", "", "", "", "");

            infobox.Show();
        }
        //Below is the update button which allows you to update the product information and cash points
        private void updateButton_Click(object sender, EventArgs e)
        {
            mycon.Open();

            string[] product_info = new string[7];

            string[] select = new string[2];

            foreach (ListViewItem item in listView1.SelectedItems)

            {//Looks for product ID for modifying per db group (Meat, Veggies, Fruit, Dairy, Dry)

                select[1] = item.Text;

                if (meatRadioButton.Checked)
                {
                    string cmdstr = "SELECT * FROM meatproducts WHERE product_id = '" + select[1] + "'";

                    cmd = new OleDbCommand(cmdstr, mycon);

                    dr = cmd.ExecuteReader();
                }

                if (fruitRadioButton.Checked)
                {
                    string cmdstr = "SELECT * FROM products WHERE product_id = '" + select[1] + "'";

                    cmd = new OleDbCommand(cmdstr, mycon);

                    dr = cmd.ExecuteReader();
                }

                if (veggiesRadioButton.Checked)
                {
                    string cmdstr = "SELECT * FROM veggiesproducts WHERE product_id = '" + select[1] + "'";

                    cmd = new OleDbCommand(cmdstr, mycon);

                    dr = cmd.ExecuteReader();
                }

                if (dairyRadioButton.Checked)
                {
                    string cmdstr = "SELECT * FROM products WHERE product_id = '" + select[1] + "'";

                    cmd = new OleDbCommand(cmdstr, mycon);

                    dr = cmd.ExecuteReader();
                }

                if (dryRadioButton.Checked)
                {
                    string cmdstr = "SELECT * FROM dryproducts WHERE product_id = '" + select[1] + "'";

                    cmd = new OleDbCommand(cmdstr, mycon);

                    dr = cmd.ExecuteReader();
                }
                while (dr.Read())

                {//Below defines the string for product information placement in the DB file. 

                    product_info[1] = (dr["product_id"].ToString());

                    product_info[2] = (dr["product_name"].ToString());

                    product_info[3] = (dr["product_srp"].ToString());

                    product_info[4] = (dr["product_cost"].ToString());

                    product_info[5] = (dr["product_stock"].ToString());

                }

            }

           

            //Below is for the form 2 add information to the DB Inventory list

            Form infobox = new Form2("update", product_info[1], product_info[2], product_info[3], product_info[4], product_info[5]);

            infobox.Show();

            dr.Close();

            mycon.Close();

            
        }
        //Removes Item fom MS Access DB file
        private void deleteButton_Click(object sender, EventArgs e)
        {
            mycon.Open();
            //Below is for when you select/ click the listed item and prepare to delete per db group (Meat, Veggies, Fruit, Dairy, Dry)

            if (meatRadioButton.Checked)
            {
                string cmdstr = "DELETE * FROM meatproducts WHERE product_id = '" + listView1.SelectedItems[0].Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();


            }
            //Below is for when you select/ click the listed item and prepare to delete per db group (Meat, Veggies, Fruit, Dairy, Dry)

            if (fruitRadioButton.Checked)
            {
                string cmdstr = "DELETE * FROM products WHERE product_id = '" + listView1.SelectedItems[0].Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //Below is for when you select/ click the listed item and prepare to delete per db group (Meat, Veggies, Fruit, Dairy, Dry)

            if (veggiesRadioButton.Checked)
            {
                string cmdstr = "DELETE * FROM veggiesproducts WHERE product_id = '" + listView1.SelectedItems[0].Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //Below is for when you select/ click the listed item and prepare to delete per db group (Meat, Veggies, Fruit, Dairy, Dry)

            if (dairyRadioButton.Checked)
            {
                string cmdstr = "DELETE * FROM dairyproducts WHERE product_id = '" + listView1.SelectedItems[0].Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //Below is for when you select/ click the listed item and prepare to delete per db group (Meat, Veggies, Fruit, Dairy, Dry)

            if (dryRadioButton.Checked)
            {
                string cmdstr = "DELETE * FROM dryproducts WHERE product_id = '" + listView1.SelectedItems[0].Text + "'";

                cmd = new OleDbCommand(cmdstr, mycon);

                cmd.ExecuteNonQuery();

                mycon.Close();
            }
            //If item is removed it will show the Item is removed with a pop up
            MessageBox.Show("Item Removed!", "Thanks!");
        }
        //Below refreshes the DB file for any chages made
        private void refreshButton_Click(object sender, EventArgs e)
        {
            mycon.Open();
            //loadbase is the function for loading DB file per db group (Meat, Veggies, Fruit, Dairy, Dry)
            if (meatRadioButton.Checked)
            {//loads stock type inventory list from created inventory function
                meatinventory();
                //closes the connection to the db

                mycon.Close();
            }

            if (fruitRadioButton.Checked)
            {//loads stock type inventory list from created inventory function
                inventory();
                //closes the connection to the db
                mycon.Close();
            }

            if (veggiesRadioButton.Checked)
            {//loads stock type inventory list from created inventory function
                veggiesinventory();
                //closes the connection to the db

                mycon.Close();
            }

            if (dairyRadioButton.Checked)
            {//loads stock type inventory list from created inventory function
                dairyinventory();
                //closes the connection to the db

                mycon.Close();
            }

            if (dryRadioButton.Checked)
            {//loads stock type inventory list from created inventory function
                dryinventory();
                //closes the connection to the db

                mycon.Close();
            }
        }
        //Exit button closes the open window
        private void exitButton_Click(object sender, EventArgs e)
        {//this will close the form
            this.Close();
        }
        //This is the Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {//opens db file connection
            mycon.Open();
            //loads stock type inventory list from created inventory function
            meatinventory();

            mycon.Close();

        }
        //Below is the function for loading the DB File
        private void inventory()

        {
            //Below clears the window from any old information and reads the current DB file to the list window. This is set up in cells to clearly define the product informaton.
            listView1.Items.Clear();

            string cmdstr = "SELECT * FROM products";

            cmd = new OleDbCommand(cmdstr, mycon);

            dr = cmd.ExecuteReader();

            string[] product_info = new string[7];

            while (dr.Read())

            {
                //Below are the strings for the cell containing product information
                product_info[1] = (dr["product_id"].ToString());

                product_info[2] = (dr["product_name"].ToString());

                product_info[3] = (dr["product_srp"].ToString());

                product_info[4] = (dr["product_cost"].ToString());

                product_info[5] = (dr["product_stock"].ToString());
                //Below prints to the listview window

                this.listView1.Items.Add(new ListViewItem(new string[] { product_info[1], product_info[2], product_info[3], product_info[4], product_info[5] }));

            }
            //Closes the reader
            dr.Close();

        }
        //Below is for loading the MEAT portion of the DB File
        private void meatinventory()

        {
            //Below clears the window from any old information and reads the current DB file to the list window. This is set up in cells to clearly define the product informaton.
            listView1.Items.Clear();

            string cmdstr = "SELECT * FROM meatproducts";

            cmd = new OleDbCommand(cmdstr, mycon);

            dr = cmd.ExecuteReader();

            string[] product_info = new string[7];

            while (dr.Read())

            {


                //Below are the strings for the cell containing product information
                product_info[1] = (dr["product_id"].ToString());

                product_info[2] = (dr["product_name"].ToString());

                product_info[3] = (dr["product_srp"].ToString());

                product_info[4] = (dr["product_cost"].ToString());

                product_info[5] = (dr["product_stock"].ToString());
                //Below prints to the listview window

                this.listView1.Items.Add(new ListViewItem(new string[] { product_info[1], product_info[2], product_info[3], product_info[4], product_info[5] }));

            }
            //Closes the reader
            dr.Close();

        }

        private void veggiesinventory()

        {
            //Below clears the window from any old information and reads the current DB file to the list window. This is set up in cells to clearly define the product informaton.
            listView1.Items.Clear();

            string cmdstr = "SELECT * FROM veggiesproducts";

            cmd = new OleDbCommand(cmdstr, mycon);

            dr = cmd.ExecuteReader();

            string[] product_info = new string[7];

            while (dr.Read())

            {
                //Below are the strings for the cell containing product information
                product_info[1] = (dr["product_id"].ToString());

                product_info[2] = (dr["product_name"].ToString());

                product_info[3] = (dr["product_srp"].ToString());

                product_info[4] = (dr["product_cost"].ToString());

                product_info[5] = (dr["product_stock"].ToString());
                //Below prints to the listview window

                this.listView1.Items.Add(new ListViewItem(new string[] { product_info[1], product_info[2], product_info[3], product_info[4], product_info[5] }));

            }
            //Closes the reader
            dr.Close();

        }

        private void dairyinventory()

        {
            //Below clears the window from any old information and reads the current DB file to the list window. This is set up in cells to clearly define the product informaton.
            listView1.Items.Clear();

            string cmdstr = "SELECT * FROM dairyproducts";

            cmd = new OleDbCommand(cmdstr, mycon);

            dr = cmd.ExecuteReader();

            string[] product_info = new string[7];

            while (dr.Read())

            {
                //Below are the strings for the cell containing product information
                product_info[1] = (dr["product_id"].ToString());

                product_info[2] = (dr["product_name"].ToString());

                product_info[3] = (dr["product_srp"].ToString());

                product_info[4] = (dr["product_cost"].ToString());

                product_info[5] = (dr["product_stock"].ToString());
                //Below prints to the listview window

                this.listView1.Items.Add(new ListViewItem(new string[] { product_info[1], product_info[2], product_info[3], product_info[4], product_info[5] }));

            }
            //Closes the reader
            dr.Close();

        }

        private void dryinventory()

        {
            //Below clears the window from any old information and reads the current DB file to the list window. This is set up in cells to clearly define the product informaton.
            listView1.Items.Clear();

            string cmdstr = "SELECT * FROM dryproducts";

            cmd = new OleDbCommand(cmdstr, mycon);

            dr = cmd.ExecuteReader();

            string[] product_info = new string[7];

            while (dr.Read())

            {
                //Below are the strings for the cell containing product information
                product_info[1] = (dr["product_id"].ToString());

                product_info[2] = (dr["product_name"].ToString());

                product_info[3] = (dr["product_srp"].ToString());

                product_info[4] = (dr["product_cost"].ToString());

                product_info[5] = (dr["product_stock"].ToString());
                //Below prints to the listview window

                this.listView1.Items.Add(new ListViewItem(new string[] { product_info[1], product_info[2], product_info[3], product_info[4], product_info[5] }));

            }
            //Closes the reader
            dr.Close();

        }
        // Picture box just being used as a style container 
        private void radioPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void meatRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb1 = sender as RadioButton;

        
            // Load DB file on check and save and quit on uncheck
            if (rb1.Checked)
            {
                // Output DB file to Rich Text Box  
                mycon.Open();
                meatinventory();
                mycon.Close();
            }
            else
            {
                // clear window  

                listView1.Text = "";
            }
        }

        private void fruitRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb2 = sender as RadioButton;


            // Load DB file on check and save and quit on uncheck
            if (rb2.Checked)
            {
                // Output DB file to Rich Text Box  
                mycon.Open();
                inventory();
                mycon.Close();
            }
            else
            {
                // clear window  

                listView1.Text = "";
            }
        }
        //Below is the radio button for the veggies foods. on check it will load its db while closing the old one.

        private void veggiesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb3 = sender as RadioButton;

            if (rb3.Checked)
            {
                // Output DB file to Rich Text Box  
                mycon.Open();
                veggiesinventory();
                mycon.Close();
            }
            else
            {
                // clear window  

                listView1.Text = "";
            }
        }
        //Below is the radio button for the dairy foods. on check it will load its db while closing the old one.

        private void dairyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb4 = sender as RadioButton;

            if (rb4.Checked)
            {
                // Output DB file to Rich Text Box  
                mycon.Open();
                dairyinventory();
                mycon.Close();
            }
            else
            {
                // clears the inventory window  

                listView1.Text = "";
            }
        }
        //Below is the radio button for the dry foods. on check it will load its db while closing the old one.
        private void dryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb5 = sender as RadioButton;

            if (rb5.Checked)
            {
                // Output DB file to Rich Text Box  
                mycon.Open();
                dryinventory();
                mycon.Close();
            }
            else
            {
                // clear window  

                listView1.Text = "";
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {//Below sets the textbox user input as a usable string for the list view
            string input;
            input = textBox1.Text;
            ListViewItem lv = listView1.FindItemWithText(input);

            if (lv == null) return; // or throw some error ....

            // move the found Item to the top of the ListView
            if (lv.Text.ToLower().Contains(textBox1.Text.ToLower())) ;
            
                listView1.Items.Remove(lv);
                listView1.Items.Insert(0, lv);
            
            // If item is found it will be highlighted in yellow
                lv.BackColor = Color.Yellow;
            // Will set the text to grey if found and highlighted
                lv.ForeColor = SystemColors.GrayText;
                lv.Selected = true;
        }
        //Below is the user input box for the search function
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
 }

