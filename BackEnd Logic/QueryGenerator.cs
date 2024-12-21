using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class QueryGenerator
{
	private readonly string[] Keys = { "c", "r", "t", "set", "block", "tou", "pow", "loy", "cmc" };
	private readonly Dictionary<string, List<string>> Values = new()
	{
		{ "c", new List<string> { "w", "u", "b", "r", "g", "wu", "bu", "ru", "gu", "bw", "br", "rw", "ru", "wub", "wug", "rgb", "rbu", "wrg", "gub", "rgu", "rwb", "rwu", "ugr", "ubrg", "wbrg", "wubg", "wubr", "wurg", "wubrg" } },
		{ "r", new List<string> { "common", "uncommon", "rare", "mythic" } },
		{ "t", new List<string> { "artifact", "battle", "conspiracy", "creature", "dungeon", "emblem", "enchantment", "hero", "instant", "kindred", "land", "phenomenon", "plane", "planeswalker", "scheme", "sorcery", "vanguard", "basic", "elite", "legendary", "ongoing", "snow", "token", "world", "attraction", "blood", "bobblehead", "clue", "contraption", "equipment", "food", "fortification", "gold", "incubator", "junk", "map", "powerstone", "treasure", "vehicle", "aura", "background", "cartouche", "case", "class", "curse", "role", "room", "rune", "saga", "shard", "shrine", "cave", "cloud", "desert", "forest", "gate", "island", "lair", "locus", "mine", "mountain", "sphere", "plains", "power-plant", "swamp", "tower", "urzas", "adventure", "arcane", "chorus", "lesson", "trap", "abian", "ajani", "aminatou", "angrath", "arlinn", "ashiok", "b.o.b.", "bahamut", "basri", "bolas", "calix", "chandra", "comet", "dack", "dakkon", "daretti", "davriel", "deb", "dihada", "domri", "dovin", "duck", "dungeon", "ellywick", "elminster", "elspeth", "equipment", "ersta", "estrid", "freyalise", "garruk", "gideon", "grist", "guff", "huatli", "inzerva", "jace", "jared", "jaya", "jeska", "kaito", "karn", "kasmina", "kaya", "kiora", "koth", "liliana", "lolth", "lukka", "luxior", "master", "minsc", "mordenkainen", "nahiri", "narset", "niko", "nissa", "nixilis", "oko", "quintorius", "ral", "rowan", "saheeli", "samut", "sarkhan", "serra", "sivitri", "sorin", "svega", "szat", "tamiyo", "tasha", "teferi", "teyo", "tezzeret", "tibalt", "tyvar", "ugin", "urza", "venser", "vivien", "vraska", "vronos", "wanderer", "will", "windgrace", "wrenn", "xenagos", "yanggu", "yanling", "zariel", "advisor", "aetherborn", "alicorn", "alien", "ally", "angel", "antelope", "ape", "archer", "archon", "armadillo", "armored", "army", "art", "artificer", "assassin", "assembly-worker", "astartes", "athlete", "atog", "aurochs", "autobot", "automaton", "avatar", "azra", "baddest,", "badger", "balloon", "barbarian", "bard", "basilisk", "bat", "bear", "beast", "beaver", "beeble", "beholder", "berserker", "biggest,", "bird", "blinkmoth", "boar", "boxer", "brainiac", "bringer", "brushwagg", "bureaucrat", "ctan", "camarid", "camel", "capybara", "caribou", "carrier", "cat", "centaur", "cephalid", "chameleon", "champion", "chef", "chicken", "child", "chimera", "citizen", "clamfolk", "cleric", "clown", "cockatrice", "construct", "cow", "coward", "coyote", "crab", "crocodile", "custodes", "cyberman", "cyborg", "cyclops", "dalek", "dauthi", "deer", "demigod", "demon", "deserter", "designer", "detective", "devil", "dinosaur", "djinn", "doctor", "dog", "donkey", "dragon", "drake", "dreadnought", "drone", "druid", "dryad", "dwarf", "efreet", "egg", "elder", "eldrazi", "elemental", "elemental?", "elephant", "elf", "elk", "elves", "employee", "etiquette", "eye", "faerie", "ferret", "fish", "flagbearer", "fox", "fractal", "frog", "fungus", "gamer", "gargoyle", "germ", "giant", "gith", "glimmer", "gnoll", "gnome", "goat", "goblin", "god", "golem", "gorgon", "grandchild", "graveborn", "gremlin", "griffin", "guest", "gus", "hag", "halfling", "hamster", "harpy", "hatificer", "hawk", "head", "hellion", "hero", "hippo", "hippogriff", "homarid", "homunculus", "hornet", "horror", "horse", "human", "hydra", "hyena", "illusion", "imp", "incarnation", "inkling", "inquisitor", "insect", "jackal", "jellyfish", "judge", "juggernaut", "kangaroo", "kavu", "killbot", "kirin", "kithkin", "knight", "kobold", "kor", "kraken", "lady", "lamia", "lammasu", "leech", "leviathan", "lhurgoyf", "licid", "lizard", "lobster", "mammoth", "manticore", "masticore", "mercenary", "merfolk", "metathran", "mime", "minion", "minotaur", "mite", "mole", "monger", "mongoose", "monk", "monkey", "moonfolk", "mount", "mouse", "mummy", "mutant", "myr", "mystic", "naga", "nastiest,", "nautilus", "necron", "nephilim", "nightmare", "nightstalker", "ninja", "noble", "noggle", "nomad", "nymph", "octopus", "ogre", "ooze", "orb", "orc", "orgg", "otter", "ouphe", "ox", "oyster", "pangolin", "paratrooper", "peasant", "pegasus", "pentavite", "performer", "pest", "phelddagrif", "phoenix", "phyrexian", "pilot", "pincher", "pirate", "plant", "pony", "porcupine", "possum", "praetor", "primarch", "prism", "processor", "proper", "rabbit", "raccoon", "ranger", "rat", "rebel", "reflection", "reveler", "rhino", "rigger", "robot", "rogue", "rukh", "sable", "salamander", "samurai", "sand", "saproling", "satyr", "scarecrow", "scientist", "scion", "scorpion", "scout", "sculpture", "serf", "serpent", "servo", "shade", "shaman", "shapeshifter", "shark", "sheep", "ship", "siren", "skeleton", "skunk", "slith", "sliver", "sloth", "slug", "snail", "snake", "soldier", "soltari", "spawn", "specter", "spellshaper", "sphinx", "spider", "spike", "spirit", "splinter", "sponge", "spuzzem", "spy", "squid", "squirrel", "starfish", "surrakar", "survivor", "synth", "teddy", "tentacle", "tetravite", "thalakos", "the", "thopter", "thrull", "tiefling", "time lord", "townsfolk", "toy", "tree", "treefolk", "trilobite", "triskelavite", "troll", "turtle", "tyranid", "unicorn", "urzan", "vampire", "vampyre", "varmint", "vedalken", "villain", "volver", "waiter", "wall", "walrus", "warlock", "warrior", "wasp", "weasel", "weird", "werewolf", "whale", "wizard", "wolf", "wolverine", "wombat", "worm", "wraith", "wrestler", "wurm", "yeti", "zombie", "zonian", "zubera", "alara", "alfava metraxis", "amonkhet", "amsterdam", "androzani minor", "antausia", "apalapucia", "arcavios", "arkhos", "azgol", "belenon", "blind eternities", "bolass meditation realm", "capenna", "chicago", "cridhe", "darillium", "dominaria", "earth", "echoir", "eldraine", "equilor", "ergamon", "fabacin", "fiora", "foldaria", "gallifrey", "gargantikar", "gobakhan", "horsehead nebula", "ikoria", "innistrad", "iquatana", "ir", "ixalan", "kaladesh", "kaldheim", "kamigawa", "kandoka", "karsus", "kephalai", "kinshala", "kolbahan", "kylem", "kyneth", "lorwyn", "luvion", "magiccon", "mars", "mercadia", "mirrodin", "moag", "mongseng", "moon", "muraganda", "necros", "new earth", "new phyrexia", "outside mutters spiral", "phyrexia", "pyrulea", "rabiah", "rath", "ravnica", "regatha", "segovia", "serra’s realm", "shadowmoor", "shandalar", "shenmeng", "skaro", "spacecraft", "tarkir", "the abyss", "the dalek asylum", "the library", "theros", "time", "trenzalore", "ulgrotha", "unknown planet", "valla", "vryn", "wildfire", "xerex", "zendikar", "zhalfir" } },
		{ "set", new List<string> { "dft", "dsk", "blb", "big", "otj", "mkm", "lci", "woe", "mat", "mom", "one", "bro", "dmu", "snc", "neo", "vow", "mid", "afr", "stx", "khm", "znr", "iko", "thb", "eld", "war", "rna", "grn", "dom", "rix", "xln", "hou", "akh", "aer", "kld", "emn", "soi", "ogw", "bfz", "dtk", "frf", "ktk", "jou", "bng", "ths", "dgm", "gtc", "rtr", "avr", "dka", "isd", "nph", "mbs", "som", "roe", "wwk", "zen", "arb", "con", "ala", "eve", "shm", "mor", "lrw", "fut", "plc", "tsb", "tsp", "csp", "dis", "gpt", "rav", "sok", "bok", "chk", "5dn", "dst", "mrd", "scg", "lgn", "ons", "jud", "tor", "ody", "apc", "pls", "inv", "pcy", "nem", "mmq", "uds", "ulg", "usg", "exo", "sth", "tmp", "wth", "vis", "mir", "all", "hml", "ice", "fem", "drk", "leg", "atq", "arn", "fdn", "m21", "m20", "m19", "ori", "m15", "m14", "m13", "m12", "m11", "m10", "10e", "9ed", "8ed", "7ed", "6ed", "5ed", "4bb", "4ed", "sum", "fbb", "3ed", "2ed", "leb", "lea", "inr", "pio", "mb2", "rvr", "cmm", "sis", "sir", "dmr", "2x2", "slx", "tsr", "klr", "plst", "akr", "2xm", "uma", "a25", "ima", "mm3", "ema", "mm2", "tpr", "vma", "mma", "me4", "me3", "me2", "me1", "ren", "rin", "bchr", "chr", "j25", "acr", "mh3", "h2r", "clu", "ltr", "j22", "clb", "dbl", "j21", "h1r", "mh2", "cmr", "jmp", "mh1", "bbd", "cn2", "cns", "gs1", "ddu", "ddt", "dds", "ddr", "ddq", "ddp", "ddo", "jvc", "dvd", "evg", "gvl", "ddn", "ddm", "ddl", "ddk", "ddj", "ddi", "ddh", "td2", "ddg", "ddf", "dde", "ddd", "ddc", "dd2", "dd1", "ydsk", "yblb", "yotj", "ymkm", "ylci", "ywoe", "yone", "ybro", "ydmu", "hbg", "ysnc", "yneo", "ymid", "oe01", "e01", "oarc", "arc", "cc2", "cc1", "cm1", "ea3", "ha7", "ea2", "slc", "gn3", "ea1", "ha6", "q07", "q06", "ha5", "ha4", "slu", "ha3", "ha2", "sld", "ha1", "gn2", "gk2", "g18", "gnt", "gk1", "e02", "g17", "md1", "ps11", "td0", "dpa", "cst", "phuk", "psal", "dkm", "psdg", "btd", "brb", "ath", "past", "mgb", "rqs", "fdc", "dsc", "blc", "m3c", "otc", "pip", "mkc", "lcc", "who", "woc", "ltc", "moc", "onc", "scd", "brc", "40k", "dmc", "ncc", "nec", "voc", "mic", "afc", "c21", "khc", "znc", "c20", "c19", "c18", "cm2", "c17", "cma", "c16", "c15", "c14", "c13", "cmd", "v17", "v16", "v15", "v14", "v13", "v12", "v11", "v10", "v09", "drb", "ph22", "da1", "ulst", "unf", "sunf", "ph21", "cmb2", "ph20", "ph19", "und", "cmb1", "ptg", "ph18", "ph17", "ust", "h17", "phtr", "hho", "unh", "ugl", "otp", "spg", "rex", "wot", "mul", "brr", "bot", "sta", "zne", "puma", "med", "mp2", "mps", "exp", "afdn", "ffdn", "fj25", "adsk", "ablb", "aacr", "amh3", "aotj", "fclu", "amkm", "alci", "awoe", "acmm", "altr", "fltr", "fmom", "amom", "fone", "aone", "fj22", "30a", "abro", "fbro", "fdmu", "admu", "aclb", "asnc", "aneo", "ovoc", "avow", "amid", "omic", "oafc", "aafr", "amh2", "oc21", "astx", "akhm", "aznr", "fjmp", "oc20", "oc19", "amh1", "oc18", "xana", "oc17", "oc16", "oc15", "oc14", "ppc1", "tdag", "thp3", "tbth", "thp2", "oc13", "tfth", "thp1", "ocm1", "phel", "ocmd", "pcmd", "olgc", "wc04", "wc03", "ovnt", "wc02", "wc01", "wc00", "wc99", "wc98", "wc97", "olep", "pmic", "pcel", "ptc", "o90p", "cei", "ced", "macr", "mone", "mbro", "mdmu", "mclb", "msnc", "mneo", "mvow", "mmid", "mafr", "mmh2", "mstx", "mkhm", "mznr", "opca", "pca", "opc2", "pc2", "ohop", "hop", "pd3", "pd2", "h09", "pjsc", "pspl", "pf25", "pfdn", "pltc", "pdsk", "plg24", "pblb", "pcbb", "pmh3", "potj", "pss4", "pmkm", "pl24", "pw24", "pf24", "plci", "pmat", "pwoe", "ptsr", "pmda", "p30t", "pf23", "pltr", "pmom", "slp", "pl23", "pone", "pr23", "p23", "pw23", "pewk", "pbro", "prcq", "p30h", "pdmu", "p30m", "p30a", "psvc", "sch", "plg22", "pclb", "ptsnc", "pncc", "psnc", "gdy", "pw22", "pl22", "pneo", "p22", "pvow", "pmid", "pafr", "plg21", "pmh2", "pw21", "pstx", "pkhm", "pl21", "pj21", "pcmr", "pznr", "pm21", "plg20", "piko", "pthb", "j20", "pf20", "peld", "pwcs", "ps19", "ppp1", "pm20", "pmh1", "pwar", "j19", "prw2", "prna", "pf19", "prwk", "pgrn", "ps18", "pana", "pm19", "pss3", "pbbd", "pdom", "plny", "pnat", "prix", "j18", "f18", "pust", "pxtc", "pxln", "pss2", "ps17", "phou", "pakh", "paer", "f17", "j17", "ps16", "pkld", "pemn", "psoi", "pogw", "j16", "f16", "pbfz", "pss1", "pori", "ps15", "ptkdf", "pdtk", "pfrf", "ugin", "j15", "f15", "pktk", "pm15", "pdp15", "ps14", "pcns", "pjou", "pbng", "j14", "f14", "pths", "pm14", "psdc", "pdgm", "wmc", "pgtc", "pdp14", "f13", "j13", "prtr", "pm13", "pavr", "pdka", "pw12", "pdp13", "pidw", "j12", "f12", "pisd", "pm12", "pnph", "pmbs", "pmps11", "pdp12", "pw11", "g11", "p11", "f11", "psom", "pm11", "proe", "pwwk", "pdp10", "pmps10", "f10", "g10", "p10", "pzen", "phop", "pm10", "parb", "purl", "pcon", "pbook", "pdtp", "pmps09", "f09", "p09", "g09", "pala", "peve", "pshm", "p15a", "pmor", "pmps08", "g08", "f08", "p08", "plrw", "p10e", "pfut", "pgpx", "ppro", "pplc", "pres", "g07", "pmps07", "f07", "p07", "ptsp", "pcsp", "pdis", "pcmp", "pgpt", "pmps06", "pal06", "pjas", "g06", "p06", "dci", "f06", "p2hg", "prav", "pmps", "p9ed", "psok", "pbok", "pal05", "pjse", "p05", "f05", "g05", "punh", "pchk", "pmrd", "p5dn", "pdst", "pal04", "g04", "p04", "f04", "p8ed", "pscg", "plgn", "pjjt", "pal03", "f03", "g03", "p03", "pons", "jp1", "prm", "pjud", "ptor", "pal02", "f02", "g02", "pr2", "pody", "papc", "ppls", "pal01", "g01", "mpr", "f01", "pinv", "ppcy", "pnem", "pelp", "pal00", "fnm", "g00", "psus", "pmmq", "pwos", "pwor", "pgru", "puds", "pptk", "pulg", "pal99", "g99", "pusg", "palp", "pexo", "psth", "jgp", "ptmp", "pred", "parl", "plgm", "pmei", "phpr", "pdrc", "ss3", "ss2", "ss1", "anb", "ajmp", "ana", "oana", "w17", "w16", "cp3", "cp2", "cp1", "s00", "s99", "ptk", "p02", "por", "itp", "tinr", "tfdn", "tdsc", "tdsk", "tblb", "tblc", "tacr", "smh3", "tmh3", "tm3c", "totj", "totc", "tbig", "totp", "tpip", "tmkc", "tmkm", "wmkm", "trvr", "trex", "slci", "tlci", "tlcc", "twho", "twoe", "twoc", "wwoe", "tcmm", "tltc", "tltr", "tmoc", "tmul", "smom", "wmom", "tmom", "tone", "tonc", "wone", "tdmr", "tscd", "t30a", "tbot", "ptbro", "sbro", "tbro", "tbrc", "tgn3", "tunf", "t40k", "ptdmu", "tdmu", "tdmc", "wdmu", "t2x2", "tclb", "tsnc", "tncc", "sneo", "tnec", "tneo", "tvow", "tvoc", "svow", "smid", "tmid", "tmic", "tafr", "tafc", "tmh2", "tc21", "tstx", "sstx", "ttsr", "skhm", "tkhc", "tkhm", "tcmr", "tznc", "tznr", "sznr", "t2xm", "tm21", "tiko", "tc20", "tund", "tthb", "tgn2", "teld", "tc19", "tm20", "tmh1", "twar", "tgk2", "trna", "tuma", "tgk1", "tgrn", "tmed", "tc18", "tm19", "tbbd", "tcm2", "tdom", "tddu", "ta25", "trix", "tust", "tima", "tddt", "txln", "te01", "tc17", "thou", "tcma", "takh", "tdds", "tmm3", "taer", "l17", "tpca", "tc16", "tkld", "tcn2", "temn", "tema", "tsoi", "togw", "l16", "tc15", "tbfz", "tori", "tmm2", "tdtk", "tfrf", "l15", "tjvc", "tdvd", "tgvl", "tevg", "tc14", "tktk", "tm15", "tcns", "tmd1", "tjou", "tddm", "tbng", "l14", "tths", "tddl", "tm14", "tmma", "tdgm", "tddk", "tgtc", "l13", "trtr", "tddj", "tm13", "tavr", "tddi", "tdka", "l12", "tisd", "tddh", "tm12", "tnph", "tddg", "tmbs", "tsom", "tddf", "tm11", "troe", "tdde", "twwk", "tddd", "tzen", "tm10", "tarb", "tddc", "tcon", "tdd2", "tala", "teve", "tshm", "tmor", "tdd1", "tlrw", "t10e", "tugl", "pz2", "pz1", "pmoa", "pvan" } },
		{ "block", new List<string> { "cmd", "cns", "mpr", "parl", "jgp", "lea", "por", "dbl", "y25", "otj", "y24", "y23", "y22", "grn", "htr", "xln", "akh", "kld", "soi", "bfz", "ktk", "ths", "rtr", "isd", "som", "zen", "ala", "shm", "lrw", "tsp", "rav", "chk", "mrd", "ons", "ody", "inv", "mmq", "usg", "tmp", "mir", "ice" } },
		{ "tou", new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" } },
		{ "pow", new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" } },
		{ "loy", new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"} },
		{ "cmc", new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" } }
	};

	public List<List<string>> GenerateKeyCombinations()
	{
		var allCombinations = new List<List<string>>();
		for (int size = 1; size <= Keys.Length; size++)
		{
			GenerateCombinations(new List<string>(), 0, size, allCombinations);
		}

		// Apply restriction: Remove invalid combinations
		return allCombinations
			.Where(combination => IsValidCombination(combination))
			.ToList();
	}

	private void GenerateCombinations(List<string> current, int index, int size, List<List<string>> allCombinations)
	{
		if (current.Count == size)
		{
			allCombinations.Add(new List<string>(current));
			return;
		}

		for (int i = index; i < Keys.Length; i++)
		{
			current.Add(Keys[i]);
			GenerateCombinations(current, i + 1, size, allCombinations);
			current.RemoveAt(current.Count - 1);
		}
	}

	private bool IsValidCombination(List<string> combination)
	{
		// Restriction: "loy" should not appear with "pow" or "tou"
		if (combination.Contains("loy") && (combination.Contains("pow") || combination.Contains("tou")))
		{
			return false;
		}
		else if (combination.Contains("set") && (combination.Contains("block")))
		{
			return false;
		}
		else
		{
			return true;
		}

		// Additional restrictions can be added here if needed
		return true;
	}

	public string GenerateRandomQuery()
	{
		var allCombinations = GenerateKeyCombinations();
		var random = new Random();

		// Randomly select one combination of keys
		var selectedCombination = allCombinations[random.Next(allCombinations.Count)];

		// Build the query using only keys
		return string.Join(" ", selectedCombination);
	}

	public void WriteAllQueriesToFile(string filePath)
	{
		var allCombinations = GenerateKeyCombinations();

		var queries = allCombinations
			.Select(combination => string.Join(" ", combination))
			.ToList();

		System.IO.File.WriteAllLines(filePath, queries);
		Console.WriteLine($"All queries written to {filePath}");
	}
}
