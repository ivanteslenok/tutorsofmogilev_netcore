import React from 'react';
import Container from '../Container';
import ContactTypesList from '../../containers/ContactTypesList';

const ContactTypesPage = () => (
  <Container headerText="Типы контактов">
    <ContactTypesList />
  </Container>
);

export default ContactTypesPage;
