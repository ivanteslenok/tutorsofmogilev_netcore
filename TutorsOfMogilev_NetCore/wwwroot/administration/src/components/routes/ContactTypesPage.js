import React from 'react';
import Container from '../Container';
import ContactTypesList from '../../containers/ContactTypesList';

export default function ContactTypesPage(params) {
  return (
    <Container headerText="Типы контактов">
      <ContactTypesList />
    </Container>
  );
}
