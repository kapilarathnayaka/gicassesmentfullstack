describe('Employee CRUD Operations', () => {
  beforeEach(() => {
    cy.visit('/employees');
  });

  it('should display employees', () => {
    cy.get('.ag-theme-material').should('be.visible');
  });

  it('should add a new employee', () => {
    cy.intercept('POST', '/api/employee').as('addEmployee');
    cy.get('button').contains('Add Employee').click();
    cy.get('input[name="name"]').type('John Doe');
    cy.get('input[name="email_address"]').type('john.doe@example.com');
    cy.get('input[name="phone_number"]').type('1234567890');
    cy.get('input[name="days_worked"]').type('5');
    cy.get('input[name="cafe"]').type('Cafe 1');
    cy.get('button').contains('Save').click();
    cy.wait('@addEmployee').its('response.statusCode').should('eq', 201);
  });

  it('should delete an employee', () => {
    cy.intercept('DELETE', '/api/employee/*').as('deleteEmployee');
    cy.get('button').contains('Delete').first().click();
    cy.on('window:confirm', () => true);
    cy.wait('@deleteEmployee').its('response.statusCode').should('eq', 200);
  });
});
