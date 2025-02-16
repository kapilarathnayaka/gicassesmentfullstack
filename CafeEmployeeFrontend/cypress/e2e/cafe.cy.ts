describe('Cafe CRUD Operations', () => {
  beforeEach(() => {
    cy.visit('/');
  });

  it('should display cafes', () => {
    cy.get('.ag-theme-material').should('be.visible');
  });

  it('should add a new cafe', () => {
    cy.intercept('POST', '/api/cafe').as('addCafe');
    cy.get('button').contains('Add Cafe').click();
    cy.get('input[name="name"]').type('New Cafe');
    cy.get('input[name="description"]').type('A new cafe description');
    cy.get('input[name="location"]').type('New York');
    cy.get('button').contains('Save').click();
    cy.wait('@addCafe').its('response.statusCode').should('eq', 201);
  });

  it('should delete a cafe', () => {
    cy.intercept('DELETE', '/api/cafe/*').as('deleteCafe');
    cy.get('button').contains('Delete').first().click();
    cy.on('window:confirm', () => true);
    cy.wait('@deleteCafe').its('response.statusCode').should('eq', 200);
  });
});
