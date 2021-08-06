Roster = function () {
    let Player = function (t) {
        let self = this;

        self.id = t.Person.Id;
        self.fullName = t.Person.FullName;
        self.jerseyNumber = t.JerseyNumber;

        self.positionName = t.Position.Name;
        self.positionType = t.Position.Type;
        self.positionAbbreviation = t.Position.Abbreviation;
    }

    let Main = function () {
        var self = this;

        self.roster = ko.observableArray([]);

        self.forwards = ko.pureComputed(function () {
            return ko.utils.arrayFilter(self.roster(), function (p) {
                return p.positionType === "Forward";
            });
        });

        self.defensemen = ko.pureComputed(function () {
            return ko.utils.arrayFilter(self.roster(), function (p) {
                return p.positionType === "Defenseman";
            });
        });

        self.goalies = ko.pureComputed(function () {
            return ko.utils.arrayFilter(self.roster(), function (p) {
                return p.positionType === "Goalie";
            });
        });

        $.post("/Team/GetRoster", { teamId: ID },
            function (r) {
                if (r.Roster && $.isArray(r.Roster)) {
                    for (let i = 0; i < r.Roster.length; i++) {
                        self.roster.push(new Player(r.Roster[i]));
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
    Roster();
});