import _ from 'lodash';

import React, { useState, useEffect } from 'react';
import Paper from '@material-ui/core/Paper';
import { EditingState } from '@devexpress/dx-react-grid';
import {
  Grid,
  Table,
  TableHeaderRow,
  TableEditRow,
  TableEditColumn
} from '@devexpress/dx-react-grid-material-ui';
import DeleteConfirmDialog from '../DeleteConfirmDialog';
import Command from '../DataGrid/Command';
import EditCell from '../DataGrid/EditCell';

const CrudDataGrid = props => {
  const [addedRows, setAddedRows] = useState([]);
  const [deletingRows, setDeletingRows] = useState([]);

  useEffect(() => {
    const { availableColumnsValuesLoaded, loadAvailableValues } = props;

    if (!availableColumnsValuesLoaded && loadAvailableValues)
      loadAvailableValues();
  }, []);

  const cancelDelete = () => setDeletingRows([]);

  const changeAddedRows = added => {
    const defaultValues = props.defaultValues || {};
    const newAddedRows = added.map(row =>
      _.keys(row).length ? row : defaultValues
    );

    setAddedRows(newAddedRows);
  };

  const commitChanges = ({ added, changed, deleted }) => {
    const { editId, create } = props;

    if (added) {
      added.forEach(item => {
        item.tutorId = editId;
        create(item);
      });
    }

    setDeletingRows(deleted || deletingRows);
  };

  const deleteRows = () => {
    deletingRows.forEach(rowId => props.remove(rowId));
    setDeletingRows([]);
  };

  let { columns } = props;

  const deleteRow =
    deletingRows[0] && _.find(props.rows, { id: deletingRows[0] });
  const deleteText = props.getDeleteText(deleteRow);

  if (props.availableColumnsValues) {
    columns.forEach(x => {
      if (props.availableColumnsValues[x.name])
        x.availableValues = props.availableColumnsValues[x.name];
    });
  }

  return (
    <Paper style={{ maxWidth: '600px' }}>
      <Grid rows={props.rows} columns={columns} getRowId={row => row.id}>
        <EditingState
          addedRows={addedRows}
          onAddedRowsChange={changeAddedRows}
          onCommitChanges={commitChanges}
        />

        <Table />
        <TableHeaderRow />

        <TableEditRow cellComponent={EditCell} />
        <TableEditColumn
          width={150}
          showAddCommand={!addedRows.length}
          showDeleteCommand
          commandComponent={Command}
        />
      </Grid>
      <DeleteConfirmDialog
        isOpen={!!deletingRows.length}
        text={deleteText}
        handleClose={cancelDelete}
        handleConfirm={deleteRows}
      />
    </Paper>
  );
};

export default CrudDataGrid;
