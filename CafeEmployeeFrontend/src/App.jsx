import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CafeList from './components/CafeList/CafeList';
import EmployeeList from './components/EmployeeList/EmployeeList';
import CafeForm from './components/CafeForm/CafeForm';
import EmployeeForm from './components/EmployeeForm/EmployeeForm';

import EditCafePage from './components/EditCafePage/EditCafePage';
import CreateCafePage from './components/CreateCafePage/CreateCafePage';

import EditEmployeePage from './components/EditEmployeePage/EditEmployeePage';
import CreateEmployeePage from './components/CreateEmployeePage/CreateEmployeePage';

function App() {
  return (
    <Router>
      <Routes>


        <Route path="/" element={<CafeList />} />
        <Route path="/cafe/create" element={<CreateCafePage />} /> {/* Add this */}
        <Route path="/cafe/edit/:id" element={<EditCafePage />} />

        <Route path="/employee/:id?" element={<EmployeeList />} />
        <Route path="/employee/create" element={<CreateEmployeePage />} /> {/* Add this */}
        <Route path="/employee/edit/:id" element={<EditEmployeePage />} />
      </Routes>
    </Router>
  );
}

export default App;
