Teams = function () {
    let Team = function (t) {
        let self = this;

        self.abbreviation = t.Abbreviation;
        self.active = t.Active;
        self.conferenceId = t.Conference.Id;
        self.conferenceName = t.Conference.Name;
        self.divisionId = t.Division.Id;
        self.divisionName = t.Division.Name;
        self.firstYearOfPlay = t.FirstYearOfPlay;
        self.id = t.Id;
        self.name = t.Name;
        self.officialSiteUrl = t.OfficialSiteUrl;
    }

    let Conference = function (c) {
        let self = this;

        self.id = c.Id;
        self.name = c.Name;

        self.divisions = ko.observableArray(c.Divisions);
    }

    let Division = function (d) {
        let self = this;

        self.id = d.Id;
        self.name = d.Name;

        self.teams = ko.observableArray([]);
    }

    let Main = function () {
        var self = this;

        self.conferences = ko.observableArray([
            new Conference({
                Id: 6,
                Name: "Eastern",
                Divisions: [
                    new Division({
                        Id: 17,
                        Name: "Atlantic"
                    }),
                    new Division({
                        Id: 18,
                        Name: "Metropolitan"
                    })
                ]
            }),
            new Conference({
                Id: 5,
                Name: "Western",
                Divisions: [
                    new Division({
                        Id: 16,
                        Name: "Central"
                    }),
                    new Division({
                        Id: 15,
                        Name: "Pacific"
                    })
                ]
            })
        ]);

        $.post("/Team/GetTeams",
            function (r) {
                if (r.Teams && $.isArray(r.Teams)) {
                    for (let i = 0; i < r.Teams.length; i++) {
                        let t = new Team(r.Teams[i]);
                        let c = self.conferences().find(c => c.id == t.conferenceId);
                        let d = c.divisions().find(d => d.id == t.divisionId);
                        d.teams.push(t);
                    }
                }
            });

        self.teams
    }

    main = new Main();
    Window.main = main;

    ko.applyBindings(main, document.getElementById("content"));
}

$(document).ready(function () {
    Teams();
});