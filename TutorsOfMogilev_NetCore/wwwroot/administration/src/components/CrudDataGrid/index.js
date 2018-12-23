import _ from 'lodash';

import React, { PureComponent } from 'react';
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

export default class CrudDataGrid extends PureComponent {
  state = {
    columns: [],
    rows: [],
    addedRows: [],
    deletingRows: []
  };

  static getDerivedStateFromProps(props) {
    return {
      columns: props.columns,
      availableColumnsValues: props.availableColumnsValues,
      defaultValues: props.defaultValues || {},
      rows: props.rows
    };
  }

  componentDidMount = () => {
    const { availableColumnsValuesLoaded, loadAvailableValues } = this.props;
    if (!availableColumnsValuesLoaded && loadAvailableValues)
      loadAvailableValues();
  };

  cancelDelete = () => this.setState({ deletingRows: [] });

  changeAddedRows = addedRows =>
    this.setState(state => {
      return {
        addedRows: addedRows.map(row =>
          _.keys(row).length ? row : state.defaultValues
        )
      };
    });

  commitChanges = ({ added, changed, deleted }) => {
    let { rows } = this.state;
    const { deletingRows } = this.state;
    const { editId, create } = this.props;

    if (added) {
      added.forEach(item => {
        item.tutorId = editId;
        create(item);
      });
    }

    this.setState({ rows, deletingRows: deleted || deletingRows });
  };

  deleteRows = () => {
    const { deletingRows } = this.state;
    deletingRows.forEach(rowId => this.props.remove(rowId));
    this.setState({ deletingRows: [] });
  };

  render() {
    const {
      rows,
      addedRows,
      deletingRows,
      availableColumnsValues
    } = this.state;

    let { columns } = this.state;

    const deleteRow = deletingRows[0] && _.find(rows, { id: deletingRows[0] });
    const deleteText = this.props.getDeleteText(deleteRow);

    if (availableColumnsValues) {
      columns.forEach(x => {
        if (availableColumnsValues[x.name])
          x.availableValues = availableColumnsValues[x.name];
      });
    }

    return (
      <Paper>
        <Grid rows={rows} columns={columns} getRowId={row => row.id}>
          <EditingState
            addedRows={addedRows}
            onAddedRowsChange={this.changeAddedRows}
            onCommitChanges={this.commitChanges}
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
          handleClose={this.cancelDelete}
          handleConfirm={this.deleteRows}
        />
      </Paper>
    );
  }
}
