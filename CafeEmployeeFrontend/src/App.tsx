import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CafePage from './pages/CafePage';
import EmployeePage from './pages/EmployeePage';

function App() {
  return (
    <Router>
      <Routes>
        {/* <Route path="/cafes" element={<CafePage />} /> */}
        <Route path="/" element={<CafePage />} />
        <Route path="/employees" element={<EmployeePage />} />
      </Routes>
    </Router>
  );
}

export default App;
