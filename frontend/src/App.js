import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import CafePage from './pages/CafePage';
import EmployeePage from './pages/EmployeePage';
import AddEditCafePage from './pages/AddEditCafePage';
import AddEditEmployeePage from './pages/AddEditEmployeePage';

function App() {
  return (
    <Router>
      <Switch>
        <Route path="/cafes" component={CafePage} />
        <Route path="/employees" component={EmployeePage} />
        <Route path="/add-edit-cafe" component={AddEditCafePage} />
        <Route path="/add-edit-employee" component={AddEditEmployeePage} />
        <Route path="/" component={CafePage} />
      </Switch>
    </Router>
  );
}

export default App;
