import React, { Component } from "react";
import { InputGroup, Button, Input, Table } from "reactstrap";

export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
    super(props);

    this.state = {
      searchQuery: "",
      results: [],
    };
  }

  handleSearchQueryChange = (event) => {
    this.setState(
      {
        searchQuery: event.target.value,
      },
      async () => {
        if (this.state.searchQuery) {
          try {
            const response = await fetch(
              `/search?query=${this.state.searchQuery}`
            );
            const jsonData = await response.json();
            console.log(jsonData);
            this.setState({
              results: jsonData,
            });
          } catch (error) {
            console.log(error);
          }
        } else {
          this.setState({
            results: []
          })
        }
      }
    );
  };

  render() {
    return (
      <div>
        <h1>Recherche Elastic Search</h1>
        <div className="col-md-4">
          <InputGroup>
            <Input
              value={this.state.searchQuery}
              onChange={this.handleSearchQueryChange}
            />
            <Button>Recherche</Button>
          </InputGroup>
        </div>
        <div className="col-md-6">
          {this.state.results.length > 0 && (
            <Table className="mt-5">
              <thead>
                <tr>
                  <th>id</th>
                  <th>Nom</th>
                  <th>Type Objet</th>
                </tr>
              </thead>
              <tbody>
                {this.state.results.map((result, index) => (
                  <tr key={index}>
                    <td>{result.sid ? result.sid : result.tid}</td>
                    <td>{result.name}</td>
                    <td>{result.sid ? "Etudiant" : "Enseignant"}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          )}
        </div>
      </div>
    );
  }
}
