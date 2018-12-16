import React from 'react';
import Grid from '@material-ui/core/Grid';
import SubjectsMultipleSelectEditor from '../../containers/SubjectsMultipleSelectEditor';
import SpecializationsMultipleSelectEditor from '../../containers/SpecializationsMultipleSelectEditor';
import PhonesCrudDataGrid from '../../containers/PhonesCrudDataGrid';
import ContactsCrudDataGrid from '../../containers/ContactsCrudDataGrid';

export default ({ row }) => {
  return (
    <div>
      <Grid container spacing={24}>
        <Grid item xs={6}>
          <SubjectsMultipleSelectEditor
            editId={row.id}
            currentValues={row.subjects}
          />
        </Grid>
        <Grid item xs={6}>
          <SpecializationsMultipleSelectEditor
            editId={row.id}
            currentValues={row.specializations}
          />
        </Grid>
        <Grid item xs={6}>
          <PhonesCrudDataGrid items={row.phones} editId={row.id} />
        </Grid>
        <Grid item xs={6}>
          <ContactsCrudDataGrid items={row.contacts} editId={row.id} />
        </Grid>
      </Grid>
    </div>
  );
};
