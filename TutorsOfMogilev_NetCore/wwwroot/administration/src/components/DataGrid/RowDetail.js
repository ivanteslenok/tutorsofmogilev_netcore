import React from 'react';
import SubjectsMultipleSelectEditor from '../../containers/SubjectsMultipleSelectEditor';
import SpecializationsMultipleSelectEditor from '../../containers/SpecializationsMultipleSelectEditor';
import PhonesCrudDataGrid from '../../containers/PhonesCrudDataGrid';
import ContactsCrudDataGrid from '../../containers/ContactsCrudDataGrid';

export default ({ row }) => {
  return (
    <>
      <div style={{ padding: '10px' }}>
        <SubjectsMultipleSelectEditor
          editId={row.id}
          currentValues={row.subjects}
        />
      </div>
      <div style={{ padding: '10px' }}>
        <SpecializationsMultipleSelectEditor
          editId={row.id}
          currentValues={row.specializations}
        />
      </div>
      <div style={{ display: 'flex' }}>
        <div style={{ padding: '10px' }}>
          <PhonesCrudDataGrid items={row.phones} editId={row.id} />
        </div>
        <div style={{ padding: '10px' }}>
          <ContactsCrudDataGrid items={row.contacts} editId={row.id} />
        </div>
      </div>
    </>
  );
};
