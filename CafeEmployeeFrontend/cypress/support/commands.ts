Cypress.Commands.add('login', (email, password) => {
  cy.visit('/login');
  cy.get('input[name="email"]').type(email);
  cy.get('input[name="password"]').type(password);
  cy.get('button[type="submit"]').click();
});

Cypress.Commands.add('addCafe', (name, description, location) => {
  cy.get('button').contains('Add Cafe').click();
  cy.get('input[name="name"]').type(name);
  cy.get('input[name="description"]').type(description);
  cy.get('input[name="location"]').type(location);
  cy.get('button').contains('Save').click();
});

Cypress.Commands.add('addEmployee', (name, email, phone, daysWorked, cafe) => {
  cy.get('button').contains('Add Employee').click();
  cy.get('input[name="name"]').type(name);
  cy.get('input[name="email_address"]').type(email);
  cy.get('input[name="phone_number"]').type(phone);
  cy.get('input[name="days_worked"]').type(daysWorked);
  cy.get('input[name="cafe"]').type(cafe);
  cy.get('button').contains('Save').click();
});
