using DevExpress.Internal.WinApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_04
{
    public partial class Form : System.Windows.Forms.Form
    {
        private List<Faculty> FacultyList = new List<Faculty>();
        public Form()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            StudentContectDB db = new StudentContectDB();
            var updateStudent = db.Students.SingleOrDefault(c => c.StudentID.Equals(txtStudentID.Text));
            updateStudent.Name = txtName.Text;
            updateStudent.FacultyID=(int)cmbFacluty.SelectedValue;
            updateStudent.AverageScore=double.Parse(txtAverageScore.Text);
            updateStudent.StudentID=txtStudentID.Text;
            db.SaveChanges();
            BindGrid(db.Students.ToList());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            StudentContectDB db=new StudentContectDB();
            cmbFacluty.ValueMember = "FacultyID";
            cmbFacluty.DisplayMember = "FacultyName";
            FillFaculty(db.Faculties.ToList());
            BindGrid(db.Students.ToList());
            

        }
        private void BindGrid(List<Student> ListStudents)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in ListStudents)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.Name;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void FillFaculty(List<Faculty> listFaculties)
        {
            this.cmbFacluty.DataSource = listFaculties;
            this.cmbFacluty.DisplayMember = "FacultyName";
            this.cmbFacluty.ValueMember = "FacultyID";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var ID= txtStudentID.Text;
            var ten=txtName.Text;
            var DTB = txtAverageScore.Text;
            var Khoa= (int)cmbFacluty.SelectedValue;
            StudentContectDB db = new StudentContectDB();

            Student student = new Student()
            {
                Name =ten,
                StudentID = ID,
                AverageScore=float.Parse(DTB),
                FacultyID=Khoa,
            };
            db.Students.Add(student);
            db.SaveChanges();
            BindGrid(db.Students.ToList());
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex >= 0)
            {
                txtName.Text = dgvStudent.Rows[RowIndex].Cells["STDName"].Value.ToString();
                txtStudentID.Text = dgvStudent.Rows[RowIndex].Cells["StdID"].Value.ToString();
                cmbFacluty.SelectedItem = dgvStudent.Rows[RowIndex].Cells["Faculty"].Value;
                txtAverageScore.Text = dgvStudent.Rows[RowIndex].Cells["AverageScore"].Value.ToString();
                cmbFacluty.Refresh();


            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            StudentContectDB db = new StudentContectDB();
            int rowIndex = dgvStudent.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                string studentID = dgvStudent.Rows[rowIndex].Cells["StdID"].Value.ToString();

                
                    var student = db.Students.FirstOrDefault(s => s.StudentID == studentID);
                    if (student != null)
                    {
                        db.Students.Remove(student);
                        db.SaveChanges();
                    }
                

                BindGrid(db.Students.ToList());
            }
        }
    }
    }

