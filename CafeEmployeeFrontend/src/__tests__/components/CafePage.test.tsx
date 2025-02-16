import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import CafePage from '../../pages/CafePage';
import { Provider } from 'react-redux';
import { store } from '../../store';

describe('CafePage Component', () => {
  test('renders CafePage component', () => {
    render(
      <Provider store={store}>
        <CafePage />
      </Provider>
    );

    expect(screen.getByText('should see the cafes here')).toBeInTheDocument();
  });

  test('delete button triggers confirmation popup', () => {
    render(
      <Provider store={store}>
        <CafePage />
      </Provider>
    );

    const deleteButton = screen.getByText('Delete');
    fireEvent.click(deleteButton);

    expect(window.confirm).toHaveBeenCalledWith('Are you sure?');
  });
});
