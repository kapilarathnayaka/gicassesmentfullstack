import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import EmployeePage from '../../pages/EmployeePage';
import { Provider } from 'react-redux';
import { store } from '../../store';

describe('EmployeePage Component', () => {
  test('renders EmployeePage component', () => {
    render(
      <Provider store={store}>
        <EmployeePage />
      </Provider>
    );

    expect(screen.getByText('Loading...')).toBeInTheDocument();
  });

  test('delete button triggers confirmation popup', () => {
    render(
      <Provider store={store}>
        <EmployeePage />
      </Provider>
    );

    const deleteButton = screen.getByText('Delete');
    fireEvent.click(deleteButton);

    expect(window.confirm).toHaveBeenCalledWith('Are you sure?');
  });
});
