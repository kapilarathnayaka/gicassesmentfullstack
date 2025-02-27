import { AgGridReact } from 'ag-grid-react';
import { Button } from '@mui/material';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { themeMaterial } from 'ag-grid-community'; 
import { useGetEmployeesByCafeIdQuery,useGetEmployeesQuery, useDeleteEmployeeMutation } from '../../api/employeeApi';
import Loader from '../Loader/Loader';

const EmployeeList = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { data: employees, isLoading=true,error,refetch } = id == null ? useGetEmployeesQuery() :  useGetEmployeesByCafeIdQuery(id);
  const [deleteEmployee] = useDeleteEmployeeMutation();

  if (isLoading) {
    return <Loader />;
  }

  if (error) {
    return <div>Error loading Employees...</div>;
  }
  

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this employee?')) {
      let response = await deleteEmployee(id);
      if(response.data.success){
        alert(response.data.message);
      }
      else{
        alert('Employee not found');
      }
      refetch();
    }
  };

  // Explicitly typing the column definitions
  const columns = [
    {
      headerName: 'Employee ID',
      field: 'id',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 1,
      },
    },
    {
      headerName: 'Name',
      field: 'name',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 1,
      },
    },
    {
      headerName: 'Email Address',
      field: 'emailAddress',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 1,
      },
    },
    {
      headerName: 'Phone Number',
      field: 'phoneNumber',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 1,
      },
    },
    {
      headerName: 'Days Worked',
      field: 'daysWorked',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 1,
      },
    },
    {
      headerName: 'Cafe Name',
      field: 'cafe',
      cellStyle: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'left',
        flex: 2,
      },
    },
    {
      headerName: 'Actions',
      field: 'id',
      cellStyle: {
        display: 'flex',          // Use flexbox for alignment
        alignItems: 'center',     // Vertically center the content
        justifyContent: 'left', // Horizontally center the content
      },
      cellRenderer: (params) => (
        <div>
          <Button color="primary" onClick={() => navigate(`/employee/edit/${params.value}`)}>Edit</Button>
          <Button color="error" onClick={() => handleDelete(params.value)}>Delete</Button>
        </div>
      ),
      flex: 2,
    }
  ];

  return (
    <div className="ag-theme-material" style={{ height: 600, width: '1500px' }}>
      <Link to="/">Back to Cafes</Link>
      <h2>Our Employees</h2>
      <div style={{ display: 'flex', justifyContent: 'space-between', padding: '10px' }}>
        <Button onClick={() => navigate('/employee/create')} variant="contained">Add New Employee</Button>
      </div>


        <AgGridReact
          rowData={employees || []}
          columnDefs={columns}
          pagination={true}
          rowHeight={50}
theme={themeMaterial}
        />
    </div>
  );
};

export default EmployeeList;
